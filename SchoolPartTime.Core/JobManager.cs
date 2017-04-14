﻿using SchoolPartTime.Common.IManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SchoolPartTime.Common.Models;
using System.Threading.Tasks;
using SchoolPartTime.Common;
using Microsoft.EntityFrameworkCore;
using SchoolPartTime.Common.ViewModels;
using SchoolPartTime.Common.QueryModels;
using SchoolPartTime.Common.Enums;

namespace SchoolPartTime.Core
{
    public class JobManager : IJobManager
    {
        private readonly ApplicationDbContext context;
        public JobManager(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 新建兼职
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task CreateAsync(Job job)
        {
            var bussiness = await context.Business.FirstOrDefaultAsync(b => b.UserId == job.UserId);
            var bussinessId = bussiness.Id;
            var time = DateTime.Now;
            job.UpdateTime = time;
            job.BusinessId = bussinessId;
            job.Status = (int)JobStatus.Underway;
            context.Job.Add(job);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 根据用户ID查兼职列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JobListView> JobList(QueryPage page, long id)
        {
            //查询总数
            var count = await context.Job.Where(b => b.UserId == id && b.Status == (int)JobStatus.Underway).CountAsync();
            string sql = @"SELECT * FROM Job WHERE userId =" + id;
            //设置排序
            sql += " AND Status = " + (int)JobStatus.Underway;
            sql += " ORDER BY id DESC";
            //设置分页数据
            sql += " LIMIT " + page.Page * page.Size + "," + page.Size;
            List<Job> jobs = await context.Job.FromSql(sql).ToListAsync();
            return new JobListView(page.Page, page.Size, count, jobs);
        }

        /// <summary>
        /// 根据id查询兼职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Job> FindById(long id)
        {
            var job = await context.Job.FirstOrDefaultAsync(c => c.Id == id);
            return job;
        }

        /// <summary>
        /// 更新兼职信息
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task Update(Job job)
        {
            var bussiness = await context.Business.FirstOrDefaultAsync(c => c.UserId == job.UserId);
            var bussinessId = bussiness.Id;
            job.BusinessId = bussinessId;
            DateTime time = DateTime.Now;
            job.UpdateTime = time;
            job.Status = 0;
            context.Job.Update(job);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 删除兼职信息
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task Delete(Job job)
        {
            context.Job.Remove(job);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 兼职详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JobModel> Details(long id, QueryPage page)
        {
            //int test = 0;
            Job job = await context.Job.SingleAsync(c => c.Id == id);
            var business = await context.Business.SingleAsync(b => b.Id == job.BusinessId);
            var name = business.Name;
            var address = business.Address;
            var tell = (await context.User.SingleAsync(a => a.Id == job.UserId)).Tell;
            var count = await context.Message.Where(b => b.JobId == id && b.ReplyId == 0).CountAsync();
            string sql = @"SELECT * FROM Message WHERE ReplyId=0";
            //设置排序
            sql += " AND JobId =" + id;
            sql += " ORDER BY id DESC";
            //设置分页数据
            sql += " LIMIT " + page.Page * page.Size + "," + page.Size;
            List<Message> list = await context.Message.FromSql(sql).ToListAsync();
            //List<Message> list = await context.Message.Where(b => b.JobId == id && b.ReplyId == 0).ToListAsync();
            List<MessageModel> messageList = new List<MessageModel>();
            for (int i = 0; i < list.Count; i++)
            {
                MessageModel model = new MessageModel(list[i]);
                model.UserStatus = (await context.User.SingleAsync(b => b.Id == list[i].WriterId)).Role;
                model.ReplyCount = await context.Message.Where(a => a.ReplyId == list[i].Id).CountAsync();
                messageList.Add(model);
            }
            JobModel jobModel = new JobModel(job);
            jobModel.BusinessName = name;
            jobModel.BusinessAddress = address;
            jobModel.Tell = tell;
            jobModel.MessageListView = new MessageListView(page.Page, page.Size, count, messageList);
            return jobModel;
        }
        /// <summary>
        /// 判断兼职是否完结
        /// </summary>
        /// <returns></returns>
        public async Task JudgeStatus()
        {
            List<Job> jobs = await context.Job.ToListAsync();
            for (int i = 0; i < jobs.Count; i++)
            {
                Job job = jobs[i];
                DateTime time = DateTime.Now;
                if (job.EndTime < time)
                {
                    await MoveJob(job.Id);
                }
            }
        }

        /// <summary>
        /// 将兼职移至完结
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task MoveJob(long jobId)
        {
            Job job = await context.Job.SingleAsync(b => b.Id == jobId);
            job.Status = (int)JobStatus.Finished;
            context.Job.Update(job);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 查询完结兼职列表
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<JobListView> OverList(long jobId, QueryPage page)
        {
            //查询总数
            var count = await context.Job.Where(b => b.UserId == jobId && b.Status == (int)JobStatus.Finished).CountAsync();
            string sql = @"SELECT * FROM Job WHERE userId =" + jobId;
            //设置排序
            sql += " AND Status="+ (int)JobStatus.Finished;
            sql += " ORDER BY id DESC";
            //设置分页数据
            sql += " LIMIT " + page.Page * page.Size + "," + page.Size;
            List<Job> jobs = await context.Job.FromSql(sql).ToListAsync();
            return new JobListView(page.Page, page.Size, count, jobs);
        }

        /// <summary>
        /// 获取兼职列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<JobListView> GetJobListAsync(JobQuery query)
        {
            string sql = @"SELECT * FROM job WHERE 1 = 1";
            //状态
            if (query.Status == (int)JobStatus.Underway || query.Status == (int)JobStatus.Finished)
                sql += " AND status = " + query.Status;
            //标题
            if (!string.IsNullOrEmpty(query.Title))
                sql += " AND title like '%" + query.Title + "%'";
            //薪资
            if (query.Salary > 0)
                sql += " AND salary >= " + query.Salary;
            //性别
            if (query.SexAsk > 0)
                sql += " AND sexAsk = " + query.SexAsk;
            //查询总数量
            int count = await context.User.FromSql(sql).CountAsync();
            //设置排序
            sql += " ORDER BY id DESC";
            //设置分页数据
            sql += " LIMIT " + query.Page * query.Size + "," + query.Size;
            List<Job> jobs = await context.Job.FromSql(sql).ToListAsync();
            //获取兼职额外信息
            List<JobModel> models = new List<JobModel>();
            foreach (var item in jobs)
            {
                //获取商家信息
                var business = await context.Business.SingleAsync(b => b.Id == item.BusinessId);
                JobModel model = new JobModel(item);
                model.BusinessName = business.Name;
                model.BusinessAddress = business.Address;
                model.Tell = (await context.User.SingleAsync(a => a.Id == item.UserId)).Tell;
                models.Add(model);
            }
            return new JobListView(query.Page, query.Size, count, models);
        }
    }
}

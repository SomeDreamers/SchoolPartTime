using SchoolPartTime.Common.IManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SchoolPartTime.Common.Models;
using System.Threading.Tasks;
using SchoolPartTime.Common;
using Microsoft.EntityFrameworkCore;
using SchoolPartTime.Common.ViewModels;

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
            var bussiness = await context.Business.FirstOrDefaultAsync(b=>b.UserId==job.UserId);
            var bussinessId = bussiness.Id;
            var time = DateTime.Now;
            job.UpdateTime = time;
            job.BusinessId = bussinessId;
            context.Job.Add(job);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 根据用户ID查兼职列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JobListView> JobList(QueryPage page,long id)
        {
            //查询总数
            var count = await context.Job.Where(b => b.UserId == id).CountAsync();
            string sql = @"SELECT * FROM Job WHERE userId ="+id;
            //设置排序
            sql += " ORDER BY id ASC";
            //设置分页数据
            sql += " LIMIT " + page.Page * page.Size + "," + page.Size;
            List<Job> jobs = await context.Job.FromSql(sql).ToListAsync();
            return new JobListView(page.Page, page.Size, count,jobs);
        }

        /// <summary>
        /// 根据id查询兼职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Job> FindById(long id)
        {
            var job =await context.Job.FirstOrDefaultAsync(c=>c.Id==id);
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
        public async Task<JobModel> Details(long id)
        {
            Job job =await context.Job.SingleAsync(c => c.Id == id);
            var business = await context.Business.SingleAsync(b => b.Id == job.BusinessId);
            var name = business.Name;
            var address = business.Address;
            var tell = (await context.User.SingleAsync(a => a.Id == job.UserId)).Tell;
            JobModel jobModel = new JobModel(job);
            jobModel.BusinessName = name;
            jobModel.BusinessAddress = address;
            jobModel.Tell = tell;
            return jobModel;
        }
    }
}

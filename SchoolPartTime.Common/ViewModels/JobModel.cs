using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class JobModel:PageViewModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 兼职标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 兼职内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 兼职发布人ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 兼职招聘人数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 兼职招聘开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 兼职招聘结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 兼职薪资
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        public long BusinessId { get; set; }

        /// <summary>
        /// 兼职标识（是否完结）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 年龄要求
        /// </summary>
        public string AgeAsk { get; set; }

        /// <summary>
        /// 性别要求
        /// </summary>
        public int SexAsk { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        //********************表外字段************************//
        /// <summary>
        /// 商家名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 商家地址
        /// </summary>
        public string BusinessAddress { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tell { get; set; }

        /// <summary>
        /// 留言列表
        /// </summary>
        public List<MessageModel> ListMessage{ get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public JobModel(int page, int size, int total) :base(page,size,total)
        {

        }

        public JobModel(int page, int size, int total,Job job):base(page,size,total)
        {
            Id = job.Id;
            BusinessId = job.BusinessId;
            Title = job.Title;
            Content = job.Content;
            Count = job.Count;
            StartTime = job.StartTime;
            EndTime = job.EndTime;
            UpdateTime = job.UpdateTime;
            Salary = job.Salary;
            SexAsk = job.SexAsk;
            AgeAsk = job.AgeAsk;
            Status = job.Status;
        }
    }
}


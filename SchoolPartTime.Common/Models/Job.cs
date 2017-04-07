using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.Models
{
    public class Job
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
        /// 兼职类型
        /// </summary>
        public int Type { get; set; }

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
    }
}

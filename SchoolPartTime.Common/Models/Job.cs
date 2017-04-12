using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "兼职标题不能为空")]
        public string Title { get; set; }

        /// <summary>
        /// 兼职内容
        /// </summary>
        [Required(ErrorMessage = "兼职内容不能为空")]
        public string Content { get; set; }

        /// <summary>
        /// 兼职发布人ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 兼职招聘人数
        /// </summary>
        [Required(ErrorMessage = "招聘人数不能为空")]
        public int Count { get; set; }

        /// <summary>
        /// 兼职招聘开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 兼职招聘结束时间
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 兼职薪资
        /// </summary>
        [Required(ErrorMessage = "薪资不能为空")]
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
        [Required(ErrorMessage = "年龄要求不能为空")]
        public string AgeAsk { get; set; }

        /// <summary>
        /// 性别要求
        /// </summary>
        [Required(ErrorMessage = "性别要求不能为空")]
        public int SexAsk { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}

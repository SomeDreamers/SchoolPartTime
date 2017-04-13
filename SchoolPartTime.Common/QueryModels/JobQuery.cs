using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.QueryModels
{
    /// <summary>
    /// 兼职查询query
    /// </summary>
    public class JobQuery : QueryPage
    {
        /// <summary>
        /// 兼职标题
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 兼职薪资
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// 兼职状态
        /// </summary>
        public int Status { get; set; }

        /// 年龄要求
        /// </summary>
        //public string AgeAsk { get; set; }

        /// <summary>
        /// 性别要求
        /// </summary>
        public int SexAsk { get; set; }
    }
}

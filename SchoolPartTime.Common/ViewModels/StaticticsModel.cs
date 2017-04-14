using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    //统计数据模型
    public class StaticticsModel
    {
        /// <summary>
        /// 用户统计数据
        /// </summary>
        public UserStatistics UserStatistics { get; set; }

        /// <summary>
        /// 兼职统计数据
        /// </summary>
        public JobStatistics JobStatistics { get; set; }
    }

    public class UserStatistics
    {
        /// <summary>
        /// 总用户数
        /// </summary>
        public int TotalUserCount { get; set; }

        /// <summary>
        /// 系统用户数
        /// </summary>
        public int SysUserCount { get; set; }

        /// <summary>
        /// 商家数
        /// </summary>
        public int BusiUserCount { get; set; }

        /// <summary>
        /// 学生用户数
        /// </summary>
        public int StudentUserCount { get; set; }
    }

    public class JobStatistics
    {
        /// <summary>
        /// 总兼职数
        /// </summary>
        public int TotalJobCount { get; set; }

        /// <summary>
        /// 进行中兼职数
        /// </summary>
        public int UnderwayJobCount { get; set; }

        /// <summary>
        /// 已结束兼职数
        /// </summary>
        public int FinishJobCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.Enums
{
    public enum JobStatus
    {
        /// <summary>
        /// 进行中
        /// </summary>
        Underway = 1,
        /// <summary>
        /// 已关闭
        /// </summary>
        Finished = 2
    }

    public static class JobStatusExtension
    {
        public static string GetDescription(this JobStatus status)
        {
            switch (status)
            {
                case JobStatus.Finished: return "已关闭";
                case JobStatus.Underway: return "进行中";
                default: return "";
            }
        }

        public static int GetValue(this JobStatus status)
        {
            return (int)status;
        }
    }
}

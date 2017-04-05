using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common
{
    public class ReturnResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        public ReturnResult()
        {
            IsSuccess = true;
        }
    }
}

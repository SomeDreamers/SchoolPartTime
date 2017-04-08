using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common
{
    public class QueryPage
    {
        public QueryPage()
        {
            Page = 0;
            Size = 5;
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int Size { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class PageViewModel
    {
        public PageViewModel(int page, int size, int total)
        {
            Page = page;
            Size = size;
            Total = total;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 总记录
        /// </summary>
        public int Total { get; set; }
    }
}

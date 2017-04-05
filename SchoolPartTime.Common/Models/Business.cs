using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.Models
{
    public class Business
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 商家名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商家描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商家地址
        /// </summary>
        public string Address { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.Models
{
    public class Message
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 兼职ID
        /// </summary>
        public long JobId { get; set; }

        /// <summary>
        /// 留言人ID
        /// </summary>
        public long WriterId { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论ID
        /// </summary>
        public long ReplyId { get; set; }

        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime Time{ get; set; }
        /// <summary>
        /// 留言人名
        /// </summary>
        public string WriterName { get; set; }
    }
}

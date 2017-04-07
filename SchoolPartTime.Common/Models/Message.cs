using System;
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
        /// 被回复人ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论ID
        /// </summary>
        public long CommentId { get; set; }
    }
}

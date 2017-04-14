using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class MessageModel
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
        public DateTime Time { get; set; }
        /// <summary>
        /// 留言人名
        /// </summary>
        public string WriterName { get; set; }

        //////////////////////////////
        /// <summary>
        /// 回复数
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 留言人角色
        /// </summary>
        public int UserStatus { get; set; }

        public MessageModel()
        {

        }
        public MessageModel(Message message)
        {
            Id = message.Id;
            WriterId = message.WriterId;
            JobId = message.JobId;
            Content = message.Content;
            ReplyId = message.ReplyId;
            WriterName = message.WriterName;
            Time = message.Time;
        }
    }
}

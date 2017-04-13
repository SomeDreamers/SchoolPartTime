using SchoolPartTime.Common.Models;
using SchoolPartTime.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPartTime.Common.IManagers
{
    public interface IMessageManager
    {
        /// <summary>
        /// 增加留言
        /// </summary>
        /// <returns></returns>
        Task AddMessage(Message message);

        /// <summary>
        /// 增加回复
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task AddReply(Message message);

        /// <summary>
        /// 回复列表
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task<ReplyModel> ToReply(long messageId);

    }
}

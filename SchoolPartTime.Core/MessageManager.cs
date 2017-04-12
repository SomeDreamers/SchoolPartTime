using SchoolPartTime.Common.IManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SchoolPartTime.Common.Models;
using System.Threading.Tasks;
using SchoolPartTime.Common;
using Microsoft.EntityFrameworkCore;
using SchoolPartTime.Common.ViewModels;

namespace SchoolPartTime.Core
{
    public class MessageManager:IMessageManager
    {
        private readonly ApplicationDbContext context;
        public MessageManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 增加留言
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task AddMessage(Message message)
        {
            DateTime time= DateTime.Now;
            message.Time=time;
            context.Message.Add(message);
            int i= await context.SaveChangesAsync();
        }

        /// <summary>
        /// 增加回复
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task AddReply(Message message)
        {
            DateTime time = DateTime.Now;
            message.Time = time;
            context.Message.Add(message);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 回复列表
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<ReplyModel> ToReply(long messageId)
        {
            List<Message> messageList = await context.Message.Where(b=>b.ReplyId==messageId).OrderByDescending(b=>b.Time).ToListAsync();
            var message = await context.Message.SingleAsync(c=>c.Id==messageId);
            ReplyModel model = new ReplyModel(message);
            model.ReplyList = messageList;
            return model;
                
        }
    }
}

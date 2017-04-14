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
            List<MessageModel> list = new List<MessageModel>();
            for(int i=0;i< messageList.Count; i++)
            {
                MessageModel messageModel = new MessageModel(messageList[i]);
                messageModel.UserStatus = (await context.User.SingleAsync(c => c.Id ==messageList[i].WriterId)).Role;
                list.Add(messageModel);
            }
            var message = await context.Message.SingleAsync(c=>c.Id==messageId);
            ReplyModel model = new ReplyModel(message);
            model.UserStatus = (await context.User.SingleAsync(b => b.Id == message.WriterId)).Role;
            model.ReplyList = list;
            return model;
                
        }
        /// <summary>
        /// 加载留言列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<MessageListView> MessageList(long id,QueryPage page)
        {
            string sql = @"SELECT * FROM Message WHERE ReplyId=0";
            //设置排序
            sql += " AND JobId =" + id;
            sql += " ORDER BY id DESC";
            //设置分页数据
            sql += " LIMIT " + page.Page * page.Size + "," + page.Size;
            List<Message> list = await context.Message.FromSql(sql).ToListAsync();
            List<MessageModel> messageList = new List<MessageModel>();
            var count = await context.Message.Where(b => b.JobId == id && b.ReplyId == 0).CountAsync();
            for (int i = 0; i < list.Count; i++)
            {
                MessageModel model = new MessageModel(list[i]);
                model.UserStatus = (await context.User.SingleAsync(b => b.Id == list[i].WriterId)).Role;
                model.ReplyCount = await context.Message.Where(a => a.ReplyId == list[i].Id).CountAsync();
                messageList.Add(model);
            }
            MessageListView listView = new MessageListView(page.Page,page.Size,count,messageList);
            return listView;
        }
    }
}

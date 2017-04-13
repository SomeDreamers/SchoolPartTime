using Microsoft.AspNetCore.Mvc;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolPartTime.WebApp.Helpers;
using SchoolPartTime.Common;
using SchoolPartTime.Common.ViewModels;

namespace SchoolPartTime.WebApp.Controllers
{
    public class MessageController:Controller
    {
        private readonly IMessageManager messageManager;
        public MessageController(IMessageManager messageManager)
        {
            this.messageManager = messageManager;
        }

        /// <summary>
        /// 发表留言
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ToMessage(long jobId,string message)
        {
            Message model = new Message();
            var id = HttpContext.User.Identity.Uid();
            var name = HttpContext.User.Identity.FullName();
            model.JobId = jobId;
            model.WriterId = id;
            model.Content = message;
            model.WriterName = name;
            await messageManager.AddMessage(model);
            ReturnResult result = new ReturnResult();
            result.Message = "失败";
            return Json(result);
        }
        /// <summary>
        /// 回复
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="message"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<IActionResult> SaveReply(long messageId,string message,long jobId)
        {
            var writerId = HttpContext.User.Identity.Uid();
            var writerName = HttpContext.User.Identity.FullName();
            Message model = new Message();
            model.Content = message;
            model.WriterId = writerId;
            model.WriterName = writerName;
            model.JobId = jobId;
            model.ReplyId = messageId;
            await messageManager.AddReply(model);
            ReturnResult result = new ReturnResult();
            result.Message = "回复失败";
            return Json(result);

        }

        public async Task<IActionResult> ToReply(long messageId)
        {
            ReplyModel model = await messageManager.ToReply(messageId);
            return View("Reply",model);
        }
    }
}

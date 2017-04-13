using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class MessageListView : PageViewModel
    {
        public MessageListView(int page, int size, int total, List<MessageModel> ListMessage) : base(page, size, total)
        {
            this.ListMessage = ListMessage;
        }

        /// <summary>
        /// 留言列表
        /// </summary>
        public List<MessageModel> ListMessage { get; set; }
    }
}

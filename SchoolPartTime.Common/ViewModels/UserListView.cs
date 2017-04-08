using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class UserListView : PageViewModel
    {
        public List<User> Users { get; set; }
        public UserListView(int page, int size, int total, List<User> users) : base(page, size, total)
        {
            Users = users;
        }
    }
}

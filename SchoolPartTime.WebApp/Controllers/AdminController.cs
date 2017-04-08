using Microsoft.AspNetCore.Mvc;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminManager adminManager;
        public AdminController(IAdminManager adminManager)
        {
            this.adminManager = adminManager;
        }

        /// <summary>
        /// 用户列表展示界面
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserList(UserQuery query)
        {
            var users = await adminManager.GetUserListAsync(query);
            return View(users);
        }
    }
}

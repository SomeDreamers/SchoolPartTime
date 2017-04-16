using Microsoft.AspNetCore.Mvc;
using SchoolPartTime.Common;
using SchoolPartTime.Common.Enums;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.Models;
using SchoolPartTime.Common.QueryModels;
using SchoolPartTime.Common.ViewModels;
using SchoolPartTime.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminManager adminManager;
        private readonly IAccountManager accountManager;
        private readonly IJobManager jobManager;
        public AdminController(IAdminManager adminManager, IAccountManager accountManager, IJobManager jobManager)
        {
            this.adminManager = adminManager;
            this.accountManager = accountManager;
            this.jobManager = jobManager;
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteUser(long id)
        {
            ReturnResult result = await adminManager.DeleteUserAsync(id);
            return Json(result);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteJob(long id)
        {
            ReturnResult result = await adminManager.DeleteJobAsync(id);
            return Json(result);
        }

        /// <summary>
        /// 新增管理员界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        /// <summary>
        /// 新增管理员
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateAdmin(UserModel userModel)
        {
            //设置管理员角色
            userModel.Role = (int)RoleType.System;
            //密码加密
            userModel.Password = EncryptionHelper.GetMD5(userModel.Password);
            //存储管理员信息
            await accountManager.RegisterAsync(userModel);
            return RedirectToAction("UserList", new { Role = userModel.Role });
        }

        /// <summary>
        /// 兼职管理页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> JobList(JobQuery query)
        {
            await jobManager.JudgeStatus();
            return View(await jobManager.GetJobListAsync(query));
        }

        /// <summary>
        /// 统计界面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Statistics()
        {
            return View(await adminManager.GetStaticticsModelAsync());
        }
    }
}

using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using SchoolPartTime.Common;
using SchoolPartTime.Common.Enums;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.Models;
using SchoolPartTime.Common.ViewModels;
using SchoolPartTime.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager accountManager;
        public AccountController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            var bol = HttpContext.User.Identity.IsAuthenticated;
            if (bol)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// 注册界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            var bol = HttpContext.User.Identity.IsAuthenticated;
            if (bol)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IActionResult> Authenticate(User user)
        {
            ReturnResult result = new ReturnResult();
            User customer = await accountManager.GetUserByNameAsync(user.Name);
            if (customer == null || customer.Password != EncryptionHelper.GetMD5(user.Password))
            {
                result.IsSuccess = false;
                result.Message = "用户名或密码错误！";
                return Json(result);
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, customer.Name, ClaimValueTypes.String), new Claim(ClaimTypes.Role, ((RoleType)customer.Role).GetDescription(), ClaimValueTypes.String), new Claim(ClaimTypes.Sid, customer.Id.ToString(), ClaimValueTypes.String) };
            var userIdentity = new ClaimsIdentity(claims, "Customer");
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            await HttpContext.Authentication.SignInAsync("IdeaCoreUser", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });
            return Json(result);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("IdeaCoreUser");
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            //密码加密
            userModel.Password = EncryptionHelper.GetMD5(userModel.Password);
            //存储用户注册信息
            await accountManager.RegisterAsync(userModel);
            //保存成功后自动登录
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userModel.Name, ClaimValueTypes.String), new Claim(ClaimTypes.Role, ((RoleType)userModel.Role).GetDescription(), ClaimValueTypes.String), new Claim(ClaimTypes.Sid, userModel.Id.ToString(), ClaimValueTypes.String) };
            var userIdentity = new ClaimsIdentity(claims, "Customer");
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            await HttpContext.Authentication.SignInAsync("IdeaCoreUser", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 验证用户名唯一性
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<bool> VerifyName(string Name)
        {
            User user = await accountManager.GetUserByNameAsync(Name);
            return user == null;
        }

        /// <summary>
        /// 商家用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> BusinessUser()
        {
            var id = HttpContext.User.Identity.Uid();
            UserModel model = await accountManager.BusinessUser(id);
            return View("BusinessUser",model);
        }

        /// <summary>
        /// 编辑商家信息
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> BusinessEdit()
        {
            var id = HttpContext.User.Identity.Uid();
            UserModel model = await accountManager.BusinessUser(id);
            return View("BusinessEdit", model);
        }

        /// <summary>
        /// 报存商家信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> SaveBusinessEdit(UserModel model)
        {
            long id = HttpContext.User.Identity.Uid();
            await accountManager.SaveBusinessEdit(id,model);
            return RedirectToAction("BusinessUser");
        }

        /// <summary>
        /// 显示修改密码页面
        /// </summary>
        /// <returns></returns>
        public IActionResult ToEditPassword()
        {
            return View("EditPassword");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditPassword(string oldPassword,string newPassword)
        {
            string oldPW = EncryptionHelper.GetMD5(oldPassword);
            string newPw = EncryptionHelper.GetMD5(newPassword);
            PasswoModel model = new PasswoModel();
            model.NewPassword = newPw;
            model.OldPassword = oldPW;
            long id = HttpContext.User.Identity.Uid();
            ReturnResult result = await accountManager.EditPassword(id,model);
            return Json(result);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SchoolPartTime.Common;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SchoolPartTime.Common.Enums;
using SchoolPartTime.Common.ViewModels;

namespace SchoolPartTime.Core
{
    public class AccountManager : IAccountManager
    {
        private readonly ApplicationDbContext context;
        public AccountManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateAsync(User user)
        {
            context.User.Add(user);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameAsync(string name)
        {
            return (await context.User.Where(c => c.Name == name).ToListAsync()).FirstOrDefault();
        }

        public Task<List<User>> QueryAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task RegisterAsync(UserModel model)
        {
            User user = model.GetUser();
            context.User.Add(user);
            await context.SaveChangesAsync();
            model.Id = user.Id;
            //商家
            if (model.Role == (int)RoleType.Business)
            {
                Business business = new Business
                {
                    UserId = user.Id,
                    Name = model.BusinessName,
                    Description = model.Description,
                    Address = model.Address
                };
                context.Business.Add(business);
            }
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 获取商家用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserModel> BusinessUser(long id)
        {
            User user = await context.User.SingleAsync(a=>a.Id==id);
            Business business = await context.Business.SingleAsync(b=>b.UserId==id);
            UserModel model = new UserModel(user);
            model.BusinessName = business.Name;
            model.Address = business.Address;
            model.Description = business.Description;
            return model;
        }

        /// <summary>
        /// 保存修改商家信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveBusinessEdit(long id,UserModel model)
        {
            User user = await context.User.SingleAsync(b=>b.Id==id);
            Business business = await context.Business.SingleAsync(c=>c.UserId==id);
            user.Name = model.Name;
            user.Tell = model.Tell;
            business.Name = model.BusinessName;
            business.Description = model.Description;
            business.Address = model.Address;
            context.User.Update(user);
            await context.SaveChangesAsync();
            context.Business.Update(business);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult> EditPassword(long id,PasswoModel model)
        {
            ReturnResult result = new ReturnResult();
            User user = await context.User.SingleAsync(b=>b.Id==id);
            string password = user.Password;
            //user.Password = model.NewPassword;
            //context.User.Update(user);
            //await context.SaveChangesAsync();
            if (password.Equals(model.OldPassword))
            {
                user.Password = model.NewPassword;
                context.User.Update(user);
                await context.SaveChangesAsync();
                result.Message = "修改成功";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "密码输入错误！";
            }
            return result;
        }
        /// <summary>
        /// 获取学生用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> StudentUser(long id)
        {
            User user = await context.User.SingleAsync(c => c.Id == id);
            return user;
        }

        /// <summary>
        /// 更新学生用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateStudent(User user)
        {
            User model = await context.User.SingleAsync(c => c.Id == user.Id);
            model.Name = user.Name;
            model.Tell = user.Tell;
            context.User.Update(model);
            await context.SaveChangesAsync();
        }
    }
}

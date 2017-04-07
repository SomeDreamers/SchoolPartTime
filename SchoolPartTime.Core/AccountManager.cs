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
    }
}

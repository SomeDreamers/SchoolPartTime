using SchoolPartTime.Common.Models;
using SchoolPartTime.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.Common.IManagers
{
    public interface IAccountManager
    {

        Task CreateAsync(User user);

        /// <summary>
        /// 查
        /// </summary>
        /// <returns></returns>
        Task<List<User>> QueryAsync();

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<User> GetUserByNameAsync(string name);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task RegisterAsync(UserModel model);
    }
}

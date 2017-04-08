using SchoolPartTime.Common.Models;
using SchoolPartTime.Common.QueryModels;
using SchoolPartTime.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPartTime.Common.IManagers
{
    /// <summary>
    /// 管理员管理接口
    /// </summary>
    public interface  IAdminManager
    {
        /// <summary>
        /// 获取用户数据集合
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<UserListView> GetUserListAsync(UserQuery queryModel);
    }
}

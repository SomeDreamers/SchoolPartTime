using Microsoft.EntityFrameworkCore;
using SchoolPartTime.Common;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.Models;
using SchoolPartTime.Common.QueryModels;
using SchoolPartTime.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPartTime.Core
{
    // <summary>
    /// 管理员管理manager
    /// </summary>
    public class AdminManager : IAdminManager
    {
        private readonly ApplicationDbContext context;
        public AdminManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region 用户管理
        /// <summary>
        /// 获取用户数据集合
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public async Task<UserListView> GetUserListAsync(UserQuery queryModel)
        {
            //context.Set<UserModel>().FromSql("");   //连表查询出新的数据模型
            string sql = @"SELECT * FROM user WHERE 1 = 1";
            //角色
            if (queryModel.Role > 0)
                sql += " AND role = " + queryModel.Role;
            //用户名
            if (!string.IsNullOrEmpty(queryModel.Name))
                sql += " AND name like '%"+ queryModel.Name + "%'";
            //电话
            if (!string.IsNullOrEmpty(queryModel.Tell))
                sql += " AND tell like '%" + queryModel.Tell + "%'";
            //查询总数量
            int count = await context.User.FromSql(sql).CountAsync();
            //设置排序
            sql += " ORDER BY id ASC";
            //设置分页数据
            sql += " LIMIT " + queryModel.Page * queryModel.Size + "," + queryModel.Size;
            List<User> users = await context.User.FromSql(sql).ToListAsync();
            return new UserListView(queryModel.Page, queryModel.Size, count, users);
        }
        #endregion
    }
}

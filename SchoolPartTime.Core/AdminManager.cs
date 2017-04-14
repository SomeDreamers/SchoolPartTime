using Microsoft.EntityFrameworkCore;
using SchoolPartTime.Common;
using SchoolPartTime.Common.Enums;
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

        /// <summary>
        /// 根据ID删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReturnResult> DeleteUserAsync(long id)
        {
            ReturnResult result = new ReturnResult();
            context.User.Remove(new User { Id = id });
            await context.SaveChangesAsync();
            return result;
        }
        #endregion

        #region 兼职管理
        /// <summary>
        /// 根据ID删除兼职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReturnResult> DeleteJobAsync(long id)
        {
            ReturnResult result = new ReturnResult();
            context.Job.Remove(new Job { Id = id });
            await context.SaveChangesAsync();
            return result;
        }
        #endregion

        #region 数据统计
        /// <summary>
        /// 获取统计数据
        /// </summary>
        /// <returns></returns>
        public async Task<StaticticsModel> GetStaticticsModelAsync()
        {
            //统计用户数据
            UserStatistics userStatistics = new UserStatistics();
            userStatistics.TotalUserCount = await context.User.CountAsync();
            userStatistics.SysUserCount = await context.User.Where(c => c.Role == (int)RoleType.System).CountAsync();
            userStatistics.StudentUserCount = await context.User.Where(c => c.Role == (int)RoleType.User).CountAsync();
            userStatistics.BusiUserCount = await context.User.Where(c => c.Role == (int)RoleType.Business).CountAsync();
            //统计兼职数据
            JobStatistics jobStatistics = new JobStatistics();
            jobStatistics.TotalJobCount = await context.Job.CountAsync();
            jobStatistics.FinishJobCount = await context.Job.Where(c => c.Status == (int)JobStatus.Finished).CountAsync();
            jobStatistics.UnderwayJobCount = await context.Job.Where(c => c.Status == (int)JobStatus.Underway).CountAsync();
            return new StaticticsModel { UserStatistics = userStatistics, JobStatistics = jobStatistics };
        }
        #endregion
    }
}

using SchoolPartTime.Common.Models;
using SchoolPartTime.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPartTime.Common.IManagers
{
    public interface IJobManager
    {
        /// <summary>
        /// 创建兼职
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Task CreateAsync(Job job);

        /// <summary>
        /// 根据用户ID查兼职列表
        /// </summary>
        /// <returns></returns>
        Task<JobListView> JobList(QueryPage page,long id);
        /// <summary>
        /// 编辑兼职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Job> FindById(long id);
        /// <summary>
        /// 更新兼职信息
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Task Update(Job job);

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        Task Delete(Job job);
        /// <summary>
        /// 查看兼职详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JobModel> Details(long id);
    }
}

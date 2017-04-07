using SchoolPartTime.Common.Models;
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
        Task<List<Job>> JobList(long id);
    }
}

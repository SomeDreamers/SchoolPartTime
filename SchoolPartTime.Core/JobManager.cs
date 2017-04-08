using SchoolPartTime.Common.IManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SchoolPartTime.Common.Models;
using System.Threading.Tasks;
using SchoolPartTime.Common;
using Microsoft.EntityFrameworkCore;

namespace SchoolPartTime.Core
{
    public class JobManager : IJobManager
    {
        private readonly ApplicationDbContext context;
        public JobManager(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 新建兼职
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task CreateAsync(Job job)
        {
            var time = DateTime.Now;
            job.UpdateTime = time;
            context.Job.Add(job);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 根据用户ID查兼职列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Job>> JobList(long id)
        {
            var jobList = await context.Job.Where(b => b.UserId == id).ToListAsync();
            return jobList;
        }
    }
}

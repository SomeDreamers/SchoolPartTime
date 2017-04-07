using Microsoft.AspNetCore.Mvc;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Controllers
{
    public class JobController:Controller
    {
        private readonly IJobManager jobManager;
        public JobController(IJobManager jobManager)
        {
            this.jobManager = jobManager;
        }

        /// <summary>
        /// 创建兼职
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 保存创建兼职
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task<IActionResult> Save(Job job)
        {
            await jobManager.CreateAsync(job);
            return View("JobList", "Job");
        }

        /// <summary>
        /// 商家我的兼职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> JobList(long id)
        {
            var jobList =await jobManager.JobList(id);
            return View("JobList", jobList);
        }
    }
}

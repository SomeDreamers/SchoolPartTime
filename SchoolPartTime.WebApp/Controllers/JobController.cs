using Microsoft.AspNetCore.Mvc;
using SchoolPartTime.Common.IManagers;
using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolPartTime.WebApp.Helpers;
using SchoolPartTime.Common;
using SchoolPartTime.Common.ViewModels;

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
            var id = HttpContext.User.Identity.Uid();
            job.UserId = id;
            await jobManager.CreateAsync(job);
            return RedirectToAction("JobList", "Job",new QueryPage());
        }

        /// <summary>
        /// 商家我的兼职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> JobList(QueryPage page)
        {
            await jobManager.JudgeStatus();
            var id = HttpContext.User.Identity.Uid();
            var jobList =await jobManager.JobList(page,id);
            return View("JobList", jobList);
        }
        /// <summary>
        /// 编辑兼职
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(long id)
        {
            var job = await jobManager.FindById(id);
            return View("Edit", job);
        }
        /// <summary>
        /// 更新兼职信息
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task<IActionResult> SaveEdit(Job job)
        {
            var id = HttpContext.User.Identity.Uid();
            job.UserId = id;
            await jobManager.Update(job);
            return RedirectToAction("JobList", "Job", new QueryPage());
        }

        /// <summary>
        /// 删除兼职信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Dalete(long id)
        {
            await jobManager.Delete(new Job { Id=id});
            ReturnResult result = new ReturnResult();
            result.Message = "删除失败";
            return Json(result);
        }

        /// <summary>
        /// 兼职详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(long id,QueryPage page)
        {
            JobModel jobModel = await jobManager.Details(id,page);
            return View("Details", jobModel);
        }
        /// <summary>
        /// 将兼职移至完结
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Over(long id)
        {
            await jobManager.MoveJob(id);
            ReturnResult result = new ReturnResult();
            result.Message = "操作失败";
            return Json(result);
        }
        /// <summary>
        /// 兼职完结列表
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IActionResult> OverList(QueryPage page)
        {
            await jobManager.JudgeStatus();
            var id = HttpContext.User.Identity.Uid();
            var jobList = await jobManager.OverList(id,page);
            return View("JobList", jobList);
        }

    }
}

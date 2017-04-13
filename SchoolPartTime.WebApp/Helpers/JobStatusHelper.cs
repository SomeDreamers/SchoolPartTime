using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPartTime.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Helpers
{
    public class JobStatusHelper
    {
        /// <summary>
        /// 获取角色下拉框
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetJobStatusItems(bool showNull = false)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (showNull)
                list.Add(new SelectListItem { Text = "请选择", Value = "0" });
            foreach (JobStatus item in Enum.GetValues(typeof(JobStatus)))
            {
                list.Add(new SelectListItem { Text = item.GetDescription(), Value = item.GetValue().ToString() });
            }
            return list;
        }
    }
}

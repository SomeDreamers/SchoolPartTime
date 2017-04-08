using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPartTime.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Helpers
{
    public static class RoleHelper
    {
        /// <summary>
        /// 获取角色下拉框
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetRoleItems(bool showNull = false)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (showNull)
                list.Add(new SelectListItem { Text = "请选择", Value = "0"});
            foreach (RoleType item in Enum.GetValues(typeof(RoleType)))
            {
                list.Add(new SelectListItem { Text = item.GetTextVaule(), Value = item.GetValue().ToString() });
            }
            return list;
        }
    }
}

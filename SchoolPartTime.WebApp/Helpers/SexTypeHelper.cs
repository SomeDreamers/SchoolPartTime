using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolPartTime.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.WebApp.Helpers
{
    public class SexTypeHelper
    {
        /// <summary>
        /// 获取性别下拉
        /// </summary>
        /// <param name="type"></param>
        /// <param name="show"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetItems(int type = 0, bool showNull = false)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (showNull)
                list.Add(new SelectListItem { Text = "不限", Value = "0" });
            foreach (SexType item in Enum.GetValues(typeof(SexType)))
            {
                list.Add(new SelectListItem { Text = item.GetDescription(), Value = item.GetValue().ToString(), Selected = type == item.GetValue() ? true : false });
            }
            return list;
        }
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSext(int type)
        {
            switch (type)
            {
                case 1: return "男";
                case 2: return "女";
                default: return "";
            }
        }
    }
}


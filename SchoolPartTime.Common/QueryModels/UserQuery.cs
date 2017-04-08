using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common.QueryModels
{
    /// <summary>
    /// 用户查询
    /// </summary>
    public class UserQuery : QueryPage
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tell { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int Role { get; set; }
    }
}

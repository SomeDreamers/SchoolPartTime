using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.Common.Enums
{
    public enum RoleType
    {
        /// <summary>
        /// 管理员
        /// </summary>
        System = 1,
        /// <summary>
        /// 用户
        /// </summary>
        User = 2,
        /// <summary>
        /// 商家
        /// </summary>
        Business = 3
    }

    public static class RoleTypeExtension
    {
        public static string GetDescription(this RoleType type)
        {
            switch (type)
            {
                case RoleType.System: return "System";
                case RoleType.User: return "User";
                case RoleType.Business: return "Business";
                default: return "";
            }
        }

        public static string GetTextVaule(this RoleType type)
        {
            switch (type)
            {
                case RoleType.System: return "管理员";
                case RoleType.User: return "学生";
                case RoleType.Business: return "商家";
                default: return "";
            }
        }

        public static int GetValue(this RoleType type)
        {
            return (int)type;
        }
    }
}

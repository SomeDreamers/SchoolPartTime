using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPartTime.Common.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum SexType
    {
        /// <summary>
        /// 男
        /// </summary>
        Man = 1,
        /// <summary>
        /// 女
        /// </summary>
        Woman = 2
    }
    public static class SexTypeExtension{
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static String GetDescription(this SexType type)
        {
            switch (type)
            {
                case SexType.Man:return "男";
                case SexType.Woman:return "女";
                default: return "不限";
            }
        }
    public static int GetValue(this SexType type)
        {
            return (int)type;
        }

    }
}

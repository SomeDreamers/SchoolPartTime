using Microsoft.AspNetCore.Mvc;
using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolPartTime.Common.ViewModels
{
    public class UserModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(8, ErrorMessage = "用户名不超过8个字符")]
        [Remote("VerifyName", "Account", ErrorMessage = "该用户名已存在！")]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "密码不能为空")]
        [MinLength(6, ErrorMessage = "密码不能小于6个字符")]
        [MaxLength(30, ErrorMessage = "密码不能超过30个字符")]
        public string Password { get; set; }

        /// <summary>
        /// 角色 
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "电话不能为空")]
        [RegularExpression("^(0|86|17951)?(13[0-9]|15[012356789]|17[0678]|18[0-9]|14[57])[0-9]{8}", ErrorMessage = "请输入正确的电话号码")]
        public string Tell { get; set; }

        /**************************表外字段*****************************/
        /// <summary>
        /// 确认密码
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入确认密码")]
        [Compare("Password", ErrorMessage = "确认密码不一致")]
        public string SurePassword { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Required(ErrorMessage = "请填写商家名称")]
        public string BusinessName { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        [Required(ErrorMessage = "请填写商家描述")]
        public string Description { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "请填写商家地址")]
        public string Address { get; set; }

        public UserModel() { }

        public UserModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
            Role = user.Role;
            Tell = user.Tell;
        }

        public User GetUser()
        {
            return new User
            {
                Id = Id,
                Name = Name,
                Password = Password,
                Role = Role,
                Tell = Tell
            };
        }
    }
}

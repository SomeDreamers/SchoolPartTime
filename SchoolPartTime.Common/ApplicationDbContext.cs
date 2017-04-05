using Microsoft.EntityFrameworkCore;
using SchoolPartTime.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPartTime.Common
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// 商家信息表
        /// </summary>
        public DbSet<Business> Business { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}

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

        /// <summary>
        /// 兼职信息表
        /// </summary>
        public DbSet<Job> Job { get; set; }

        /// <summary>
        /// 留言表
        /// </summary>
        public DbSet<Message> Message { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}

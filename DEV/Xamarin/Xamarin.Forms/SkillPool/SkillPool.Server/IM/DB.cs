using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SkillPool.Server.IM
{
    /// <summary>
    /// 数据库访问上下文,建立实体类与数据库连接的桥梁
    /// </summary>
    public class Db : DbContext
    {
        public Db() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                //注意，json配置文件是位于调用此函数的项目内（Web项目）
                //var builder = new ConfigurationBuilder()
                //    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("DB.json");
                //var config = builder.Build();

                //string conString = config.GetSection("ConnectionStrings:DevContext").Value; // 分层键
                //optionsBuilder.UseMySQL(conString);
                optionsBuilder.UseMySQL(@"server=120.79.67.39;uid=root;pwd=;
                    port=3306;database=IM;sslmode=Preferred;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<IM_MSG_CONTACT> MSgCONTACTs { get; set; }
        /// <summary>
        /// 联系人列表
        /// </summary>
        public virtual DbSet<IM_CONTACT> IM_CONTACT { get; set; }
        public virtual DbSet<IM_MSG_CONTENT> IM_MSG_CONTENT { get; set; }
        public virtual DbSet<IM_MSG_RELATION> IM_MSG_RELATION { get; set; }
        public virtual DbSet<IM_USER> IM_USER { get; set; }
    }
}

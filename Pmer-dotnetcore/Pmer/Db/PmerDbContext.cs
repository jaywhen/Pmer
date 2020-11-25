using Microsoft.EntityFrameworkCore;
using Pmer.Models;
using System.Configuration;
using System.IO;
namespace Pmer.Db
{
    public class PmerDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        // 数据库的存放位置
        // private string dbPath = ConfigurationManager.AppSettings["DataBaseFileNamePath"];

        public DbSet<MainPassword> MainPassword { get; set; }
        public DbSet<PasswordItem> PasswordItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source= {ConfigurationManager.AppSettings["DataBaseFileNamePath"]} ");

    }
}

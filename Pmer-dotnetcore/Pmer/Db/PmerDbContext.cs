using Microsoft.EntityFrameworkCore;
using Pmer.Models;
using System.Configuration;

namespace Pmer.Db
{
    public class PmerDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<MainPassword> MainPassword { get; set; }
        public DbSet<PasswordItem> PasswordItems { get; set; }
        public DbSet<Tag> Tags { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source= {ConfigurationManager.AppSettings["DataBaseFileNamePath"]} ");
    }
}

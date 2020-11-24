using Pmer.Models;
using System.IO;
using System.Linq;

namespace Pmer.Db
{
    // 数据库帮助类
    public static class DbHelper
    {
        static bool ContianData()
        {
            // 检查MainPassword中是否含有数据
            PmerDbContext dbContext = new PmerDbContext();
            int item_nums = dbContext.MainPassword.ToList().Count();
            return item_nums > 0 ? true : false;
        }

        // 
        /// <summary>
        /// 判断用户是否已经注册
        /// </summary>
        /// <returns>已经注册返回true， 否则返回false</returns>
        public static bool IfRegister()
        {
            string dbDirectory = System.Environment.CurrentDirectory + "\\Db";
            string dbFile = dbDirectory + "\\PmerDb.db";
            using(var db = new PmerDbContext())
            {
                if (!Directory.Exists(dbDirectory))
                {
                    // 若不存在数据库文件夹则创建文件夹与数据表
                    Directory.CreateDirectory(dbDirectory);
                    db.Database.EnsureCreated();
                    return false;
                }
                else if (!File.Exists(dbFile))
                {
                    // 存在文件夹不存在数据库文件
                    db.Database.EnsureCreated();
                    return false;
                }
                else if (!ContianData())
                {
                    // 存在数据库文件，表中无数据
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 将主密码以及用户名和盐插入MainPassword表中
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="preSalt"></param>
        /// <param name="sufSalt"></param>
        /// 改进：添加错误处理
        public static void InsertMainPassword(string username, string password, string preSalt, string sufSalt)
        {
            MainPassword mainPassword = new MainPassword
            {
                Username = username,
                Password = password,
                PreSalt = preSalt,
                SufSalt = sufSalt
            };
            PmerDbContext dbContext = new PmerDbContext();
            dbContext.Add(mainPassword);
            dbContext.SaveChanges();
        }

        public static MainPassword GetMainPasswordItem()
        {

            PmerDbContext dbContext = new PmerDbContext();

            // 此处因为主密码表中只有一个id，且一定值为1，故如此查询
            // 后期维护改进
            MainPassword mainPassword = dbContext.MainPassword.Single(p => p.Id == 1);
            return mainPassword;

        }

    }
}

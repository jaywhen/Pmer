using Pmer.Encryption;
using Pmer.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Collections.ObjectModel;

namespace Pmer.Db
{
    // 数据库帮助类
    public static class DbHelper
    {
        // 
        /// <summary>
        /// 判断用户是否已经注册
        /// </summary>
        /// <returns>已经注册返回true， 否则返回false</returns>
        public static bool IfRegister()
        {
            string dbDirectory = ConfigurationManager.AppSettings["DataBaseDirNamePath"];
            string dbFile = ConfigurationManager.AppSettings["DataBaseFileNamePath"];

            using (var db = new PmerDbContext())
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
                else if (!IfMPContainData())
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

        #region Insert

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
            // dbContext.Add(mainPassword); // 修改过
            dbContext.MainPassword.Add(mainPassword);

            dbContext.SaveChanges();
        }

        /// <summary>
        /// 将添加的密码项插入表中
        /// </summary>
        /// <param name="passwordItem"></param>
        public static void InsertPasswordItem(PasswordItem passwordItem)
        {
            PmerDbContext dbContext = new PmerDbContext();

            // 修改过
            dbContext.PasswordItems.Add(passwordItem);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// 插入 tag name 返回 tag id
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns>tag id</returns>
        public static int InsertTag(string tagName)
        {
            PmerDbContext dbContext = new PmerDbContext();
            Tag tg = new Tag
            {
                TagName = tagName
            };
            dbContext.Tags.Add(tg);
            dbContext.SaveChanges();

            var lastTag = dbContext.Tags.OrderBy(tg => tg.TagId).Last();
            return lastTag.TagId;
        }

        #endregion

        #region Update
        public static void UpdatePasswordItem(PasswordItem passwordItem)
        {
            PmerDbContext dbContext = new PmerDbContext();
            dbContext.PasswordItems.Update(passwordItem);
            dbContext.SaveChanges();
        }
        #endregion

        #region Search
        /// <summary>
        /// 获取所有的密码项
        /// </summary>
        /// <param name="keyPassword">用于解密的密钥</param>
        /// <returns></returns>
        public static List<PasswordItem> GetAllPasswordItems(byte[] keyPassword)
        {
            PmerDbContext dbContext = new PmerDbContext();
            List<PasswordItem> passwordItems = dbContext.PasswordItems.ToList();
            passwordItems = DecrypPasswordItem(passwordItems, keyPassword);
            return passwordItems;
        }

        public static MainPassword GetMainPasswordItem()
        {

            PmerDbContext dbContext = new PmerDbContext();

            // 此处因为主密码表中只有一个id，且一定值为1，故如此查询
            // 后期维护改进
            MainPassword mainPassword = dbContext.MainPassword.Single(p => p.Id == 1);
            return mainPassword;
        }

        public static int GetTagId(string TagNameAdded)
        {
            PmerDbContext dbContext = new PmerDbContext();
            var tagItem = dbContext.Tags.Single(tg => tg.TagName.Equals(TagNameAdded));
            return tagItem.TagId;
        }

        /// <summary>
        /// 返回 Tags 表中的所有内容
        /// </summary>
        /// <returns></returns>
        public static List<Tag> GetAllTags()
        {
            PmerDbContext dbContext = new PmerDbContext();
            var retList = dbContext.Tags.ToList();
            return retList;
        }


        /// <summary>
        /// 从库中取回密码项，并对密码解密
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns>明文密码项目</returns>
        public static List<PasswordItem> GetAllPasswordItemFromTag(int tagId, byte[] keyPassword)
        {
            PmerDbContext dbContext = new PmerDbContext();
            var passwords = dbContext.PasswordItems.Where(pw => pw.TagId == tagId).ToList();
            passwords = DecrypPasswordItem(passwords, keyPassword);
            return passwords;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除置顶 Id 的密码项
        /// </summary>
        /// <param name="id">密码项目的 Id</param>
        public static void DeletePasswordItem(int id)
        {
            PmerDbContext dbContext = new PmerDbContext();
            PasswordItem passwordItem = dbContext.PasswordItems.Find(id);
            dbContext.PasswordItems.Remove(passwordItem);
            dbContext.SaveChanges();
        }

        #endregion

        #region Tools
        // 一些工具函数
        /// <summary>
        /// MP: MainPassword
        /// </summary>
        /// <returns></returns>
        private static bool IfMPContainData()
        {
            // 检查MainPassword中是否含有数据
            PmerDbContext dbContext = new PmerDbContext();
            int item_nums = dbContext.MainPassword.ToList().Count();
            return item_nums > 0 ? true : false;
        }

        /// <summary>
        /// 对含有密文密码属性的 passwordItem 解密
        /// </summary>
        /// <param name="passwordItems"></param>
        /// <param name="keyPassword"></param>
        /// <returns></returns>
        private static List<PasswordItem> DecrypPasswordItem(List<PasswordItem> passwordItems, byte[] keyPassword)
        {
            for (int i = 0; i < passwordItems.Count(); i++)
            {
                passwordItems[i].Password = Encryptor.AESDecrypt(passwordItems[i].Password, keyPassword);
            }
            return passwordItems;
        }

        /// <summary>
        /// 查找 TagList 是否包含 tag 名为
        /// tagName 的元素
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static bool IfTagedPasswordListContain(ObservableCollection<Tag> tags, string tagName)
        {
            if (tags == null) return false;
            foreach(Tag td in tags)
            {
                if (td.TagName.Equals(tagName))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion


    }
}

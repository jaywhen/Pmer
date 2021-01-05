using Pmer.Db;
using Pmer.Handler;
using Pmer.Models;
using System.Collections.Generic;
using System.IO;
using Pmer.Encryption;


namespace Pmer.Helper
{
    /// <summary>
    /// 文件读写处理类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 将 *.csv 文件中的密码转换为一个Tag
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Tag ChromeCSVToTag(string filePath, string tagName, byte[] key)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            // 跳过第一行(已知第一行的内容)
            reader.ReadLine();
            // chrome csv pw file head: 
            // [0]name(Title) [1]url(website) [2]username(account) [3]password
            string line;
            List<PasswordItem> passwordItems = new List<PasswordItem>();
            int tagId = DbHelper.GetMaxTagId() + 1;
            while((line = reader.ReadLine()) != null)
            {
                string[] lines = line.Split(',');
                PasswordItem passwordItem = new PasswordItem 
                { 
                    Title = lines[0],
                    Account = lines[2],
                    Password = Encryptor.AESEncrypt(lines[3], key),
                    Website = lines[1],
                    Avatar = AvatarDictionary.GetAvatarPath(lines[0]) ,
                    TagId = tagId
                };
                passwordItems.Add(passwordItem);
            }
            reader.Close();

            Tag retTag = new Tag
            {
                PasswordItems = passwordItems,
                TagId = tagId,
                TagName = tagName
            };
            return retTag;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pmer.Encryption
{
    /// <summary>
    /// 密码加密解密、登录密码哈希函数
    /// </summary>
    public static class Encryptor
    {
        public static string GenerateSalt()
        {
            // 生成字符集
            string strSetNum = "0123456789";
            string strSetAlphLower = "abcdefghijklmnopqrstuvwxyz";
            string strSetAlphUpper= strSetAlphLower.ToUpper();
            string strSetPrefix = "ThisIs";
            string strSetSuffix = "PiMer!";

            string strSet = strSetPrefix + strSetNum + strSetAlphLower + strSetAlphUpper + strSetSuffix;

            // 
            StringBuilder saltItem = new StringBuilder();
            var rand = new Random();
            for (int i = 0; i<8; ++i)
            {
                saltItem.Append(strSet[rand.Next(strSet.Length)]);
            }
            return saltItem.ToString();
        }


        public static string SHA512AddSalt(string preSalt,string rawPassword, string sufSalt)
        {
            // 对原串首尾加盐再哈希

            string addedSalt = preSalt + rawPassword + sufSalt;

            byte[] passwordByte = Encoding.UTF8.GetBytes(addedSalt);

            SHA512 sha = new SHA512CryptoServiceProvider();
            byte[] addedSaltByte = sha.ComputeHash(passwordByte);
            StringBuilder sha512ContainSalt = new StringBuilder();
            foreach(byte ss in addedSaltByte)
            {
                // x2代表转为小写的十六进制
                sha512ContainSalt.Append(ss.ToString("x2"));
            }

            return sha512ContainSalt.ToString();
        }

    }
}

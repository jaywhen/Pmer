using System;
using System.Security.Cryptography;
using System.Text;

namespace Pmer.Encryption
{
    /// <summary>
    /// 密码加密解密、登录密码哈希函数
    /// </summary>
    public static class Encryptor
    {
        /// <summary>
        /// 将字节数组转换为小写十六进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>已转换的字符串</returns>
        public static string BytesToString(byte[] bytes)
        {
            StringBuilder convertedStr = new StringBuilder();
            foreach(byte b in bytes)
            {
                convertedStr.Append(b.ToString("x2"));
            }
            return convertedStr.ToString();
        }

        /// <summary>
        /// 生成字符串盐
        /// </summary>
        /// <returns>盐</returns>
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

        /// <summary>
        /// 将明文使用SHA512加盐进行加密
        /// </summary>
        /// <param name="preSalt">加在明文前缀部分的盐</param>
        /// <param name="rawPassword">明文</param>
        /// <param name="sufSalt">加在明文后缀部分的盐</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA512AddSalt(string preSalt,string rawPassword, string sufSalt)
        {
            // 对原串首尾加盐再哈希

            string addedSalt = preSalt + rawPassword + sufSalt;

            byte[] passwordByte = Encoding.UTF8.GetBytes(addedSalt);

            SHA512 sha = new SHA512CryptoServiceProvider();
            byte[] addedSaltByte = sha.ComputeHash(passwordByte);

            string sha512ContainSalt = BytesToString(addedSaltByte);
            return sha512ContainSalt.ToString();
        }

        /// <summary>
        /// 对明文进行AES加密（ECB模式）
        /// </summary>
        /// <param name="plianText">明文</param>
        /// <param name="key">key 必须是16byte的字节数组</param>
        /// <returns></returns>
        public static string AESEncrypt(string plianText, byte[] key)
        {
            if (key.Length < 16)
                throw new ArgumentException("The key is not valid");

            byte[] plianTextArray = Encoding.UTF8.GetBytes(plianText);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            //rDel.Key = key;
            //rDel.Mode = CipherMode.ECB;
            //rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resulArray = cTransform.TransformFinalBlock(plianTextArray, 0, plianTextArray.Length);
            return Convert.ToBase64String(resulArray);
        }

        /// <summary>
        /// 对密文进行解密（AES）
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string AESDecrypt(string cipherText, byte[] key)
        {
            byte[] cipherTextArray = Convert.FromBase64String(cipherText);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] plianTextArray = cTransform.TransformFinalBlock(cipherTextArray, 0, cipherTextArray.Length);
            return Encoding.UTF8.GetString(plianTextArray);
        }

        /// <summary>
        /// 对key进行一遍MD5哈希，
        /// 使其满足16byte的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] HashedKeyByMD5(string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashedKey = md5.ComputeHash(keyArray);
            return hashedKey;
        }
    }
}

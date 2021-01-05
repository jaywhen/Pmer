using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Pmer.Handler
{
    public static class AvatarDictionary
    {
        /// <summary>
        /// 返回图片资源的绝对路径
        /// </summary>
        /// <param name="avatarName"></param>
        /// <returns></returns>
        public static string GetAvatarPath(string avatarName)
        {
            avatarName = ConfigurationManager.AppSettings[avatarName.ToLower()] ?? ConfigurationManager.AppSettings["default"];
            string retAvatar = ConfigurationManager.AppSettings["AvatarRoot"] + avatarName;
            return retAvatar;
        }
    }
}

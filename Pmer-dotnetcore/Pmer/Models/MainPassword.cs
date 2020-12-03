using System;
using System.Collections.Generic;
using System.Text;

namespace Pmer.Models
{
    /// <summary>
    /// 主密码Model
    /// </summary>
    public class MainPassword
    {
        public int Id { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }
        public string PreSalt { get; set; }
        public string SufSalt { get; set; }
    }
}

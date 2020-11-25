using System;
using System.Collections.Generic;
using System.Text;

namespace Pmer.Models
{
    public class PasswordItem
    {
        public PasswordItem() { }
        public PasswordItem(int id, string title, string account, string password, string website, string avatar)
        {
            this.Id = id;
            this.Title = title;
            this.Account = account;
            this.Password = password;
            this.Website = website;
            this.Avatar = avatar;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Website { get; set; }
        public string Avatar { get; set; }
    }
}

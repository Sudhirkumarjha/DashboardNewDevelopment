using System;
using System.Collections.Generic;
using System.Text;

namespace VideoAssetManager.DataAccess.Common
{
    public class MailRecipient
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public int UserID { get; set; }

        public MailRecipient()
        {

        }

        public MailRecipient(string email)
        {
            Email = email;
        }
        public MailRecipient(string email, string name) : this(email)
        {
            Name = name;
        }
        public MailRecipient(string email, string name, int userId) : this(email, name)
        {
            UserID = userId;
        }
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Name) ? $"{Email}" : $"{Name} <{Email}>";
        }
    }
}

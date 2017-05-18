using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.ViewModels
{
    public class MessageUsersViewModel
    {
        public int SenderID { get; set; }
        public List<UserForMessage> Users {get;set;}

        public MessageUsersViewModel()
        {
            Users = new List<UserForMessage>();
        }
    }
}
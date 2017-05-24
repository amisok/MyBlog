using ASPApp_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.ViewModels
{
    public class MyMessage
    {
        public int UserFromID { get; set; }
        public string UserFromName { get; set; }
        public string UserFromSurname { get; set; }

        public int UserToID { get; set; }
        public string UserToName { get; set; }
        public string UserToSurname { get; set; }

        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public int MessageID { get; set; }

        public MyMessage()
        { }

        public MyMessage(MessageToUser mtu)
        {
            UserFromID = mtu.UserFrom.ID;
            UserFromName = mtu.UserFrom.Name;
            UserFromSurname = mtu.UserFrom.Surname;
            UserToID = mtu.UserTo.ID;
            UserToName = mtu.UserTo.Name;
            UserToSurname = mtu.UserTo.Surname;
            Text = mtu.Message.Text;
            CreationTime = mtu.Message.CreationTime;
        }
    }
}
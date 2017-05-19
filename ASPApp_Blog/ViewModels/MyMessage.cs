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

    }
}
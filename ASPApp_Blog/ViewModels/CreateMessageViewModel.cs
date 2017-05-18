using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.ViewModels
{
    public class CreateMessageViewModel
    {
        public int UserFromID { get; set; }
        public int UserToID { get; set; }
        public string UserToName { get; set; }
        public string UserToSurname { get; set; }
    }
}
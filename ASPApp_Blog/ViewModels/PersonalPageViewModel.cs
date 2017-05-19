using ASPApp_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.ViewModels
{
    public class PersonalPageViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string Email { get; set; }
        public System.DateTime CreationTime { get; set; }
       
    }
}
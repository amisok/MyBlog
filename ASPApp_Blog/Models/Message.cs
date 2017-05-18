using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.Models
{
    public class Message
    {
       
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }

       
    }
}
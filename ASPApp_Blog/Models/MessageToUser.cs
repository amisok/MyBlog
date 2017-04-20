using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.Models
{
    public class MessageToUser
    {
        public int ID { get; set; }
        
        public virtual Message Message { get; set; }
        public virtual User UserTo { get; set; }
       
    }
}
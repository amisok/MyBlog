using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ASPApp_Blog.Models
{
    public class User
    {
   
        public int ID { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }

        public Nullable<int> Age { get; set; }
        
        public string Email { get; set; }

        public System.DateTime CreationTime { get; set; }

        

        
    }
}
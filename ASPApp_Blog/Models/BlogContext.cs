using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.Models
{
    public class BlogContext:DbContext
    {
       
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageToUser> MessageToUsers{ get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
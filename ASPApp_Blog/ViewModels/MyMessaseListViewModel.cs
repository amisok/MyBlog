using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.ViewModels
{
    public class MyMessaseListViewModel
    {
       public int ID { get; set; }
       public IEnumerable<MyMessage> OutputMessage { get; set; }
       public IEnumerable<MyMessage> InputMessage { get; set; }

    }
}
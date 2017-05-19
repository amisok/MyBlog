using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPApp_Blog.ViewModels
{
    public class MyMessaseListViewModel
    {
        public int ID { get; set; }
       public List<MyMessage> OutputMessage { get; set; }
       public List<MyMessage> InputMessage { get; set; }

        public MyMessaseListViewModel()
        {
            OutputMessage = new List<MyMessage>();
            InputMessage = new List<MyMessage>();
        }
    }
}
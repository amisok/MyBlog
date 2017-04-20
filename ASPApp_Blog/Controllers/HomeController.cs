using ASPApp_Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPApp_Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult Enter(string login, string password)
        {
           
            using (BlogContext db = new BlogContext())
            {
                foreach (User u in db.Users)
                {
                    if (u.Login==login)
                    {
                        if (u.Password == password)
                        {
                           
                            return RedirectToAction("PersonalPage", "Personal", new { id = u.ID });
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Password is wrong");
                            return View("Index");
                        }
                    }
                
                }
                ModelState.AddModelError("Login", "There is no such user. Please register");
                return View("Index");

            }
  
        }
       
    }
}
using ASPApp_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPApp_Blog.Controllers
{
    public class MessageController : Controller
    {
        [HttpGet]
        public ActionResult Create(int id=0)
        {
            using(BlogContext db = new BlogContext())
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            
        }
        [HttpPost]
        public ActionResult CreateMessage( string login, string text, int id=0)
        {
            string[] logins = login.Split(new string[] { ",", ", "},StringSplitOptions.RemoveEmptyEntries);

            Message message = new Message();
            message.CreationTime = DateTime.Now;
            message.Text = text;
            MessageToUser messageToUser = new MessageToUser();
            using (BlogContext db = new BlogContext())
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                message.UserFrom = user;
                db.Messages.Add(message);
                messageToUser.Message = message;

                foreach (string item in logins)
                {
                    try
                    {
                        messageToUser.UserTo = db.Users.Where(u => u.Login == item).Single();
                    }
                    catch(Exception)
                    {
                       ModelState.AddModelError("Login", "Unknown user");
                       return View("Create", user);
                    }
                    db.MessageToUsers.Add(messageToUser);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("PersonalPage", "Personal", new { id=id});
        }
    }
}
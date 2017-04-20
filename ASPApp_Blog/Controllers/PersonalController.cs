using ASPApp_Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ASPApp_Blog.Controllers
{
    public class PersonalController : Controller
    {
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {

            CheckField(user);
            using (BlogContext db = new BlogContext())
            {
                user.CreationTime = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("PersonalPage", user);

                }

                return View(user);
            }
        }
       
        public ActionResult PersonalPage(int id)
        {
            User user = FindReturnUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
           FindMessages(user);
            return View(user);
        }

        public void FindMessages(User user)
        {
           
            List<Message> myMessages = new List<Message>();
           
            using (BlogContext db = new BlogContext())
            {
                foreach (var item in db.Messages)
                {
                    
                        myMessages.Add(item);
                  
                    
                }  
            }
            ViewBag.MyMessages = myMessages;
           
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = FindReturnUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            CheckField(user);
            using (BlogContext db=new BlogContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("PersonalPage",user);
                    
                }
               
                return View(user);
            }
        }
        
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            User user = FindReturnUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            using (BlogContext db = new BlogContext())
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }


                //TODO: Rewrite
                foreach (var msg in db.Messages)
                {
                    if (msg.UserFrom == user)
                    {

                        foreach (var msgTo in db.MessageToUsers)
                        {
                            if (msg == msgTo.Message)
                            {
                                db.MessageToUsers.Remove(msgTo);
                            }
                        }
                        db.Messages.Remove(msg);
                    }
                }

                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
        }

        public User FindReturnUser(int id)
        {
            using (BlogContext db = new BlogContext())
            {
                User user = db.Users.Find(id);

                return user;
            }
        }

        public void CheckField(User user)
        {
            using (BlogContext db = new BlogContext())
            {
                foreach (User item in db.Users)
                {
                    if (user.ID != item.ID)
                    {
                        if (user.Login == item.Login)
                        {
                            ModelState.AddModelError("Login", "This login already exists");
                            return;
                        }
                        if (user.Password == item.Password)
                        {
                            ModelState.AddModelError("Password", "This password already exists");
                            return;
                        }
                        if (user.Email == item.Email)
                        {
                            ModelState.AddModelError("Email", "This e-mail already exists");
                            return;
                        }
                    }

                }


            }


        }

    }
}
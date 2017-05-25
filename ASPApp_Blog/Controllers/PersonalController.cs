using ASPApp_Blog.Models;
using ASPApp_Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System. Web.Security;

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
        public ActionResult Registration(UserViewModel model)
        {
            CheckField(model);
            using (BlogContext db = new BlogContext())
            {
                if (ModelState.IsValid)
                {
                    User user = ModelToUser(model);
                    user.CreationTime = DateTime.Now;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("PersonalPage", new { id = user.ID });
                }
                return View(model);
            }
        }

        public ActionResult PersonalPage(int id)
        {
            User user = FindReturnUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            PersonalPageViewModel model = UserToPersonalModel(user);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = FindReturnUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserViewModel model = UserToModel(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            
            CheckField(model);
            using (BlogContext db = new BlogContext())
            {
                if (ModelState.IsValid)
                {
                    User user = ModelToUser(model);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("PersonalPage", new { id = user.ID });
                }
                return View(model);
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

            DeleteUserViewModel model = UserToDeleteModel(user);
            return View(model);
        }


        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            using (BlogContext db = new BlogContext())
            {
                User user = FindReturnUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var msg = db.MessageToUsers
                            .Where(m => m.UserFrom.ID == user.ID || m.UserTo.ID == user.ID)
                            .Select(m=>m);

                foreach (var item in msg)
                {
                    db.Messages.Remove(item.Message);
                    db.MessageToUsers.Remove(item);
                }
                db.Users.Attach(user);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        
        public ActionResult MyMessageList(int id)
        {
            User user = FindReturnUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            MyMessaseListViewModel model = new MyMessaseListViewModel();
            model.ID = user.ID;
            model.InputMessage = FindInputMessages(user);
            model.OutputMessage = FindOutputMessages(user);
            return View(model);
        }


        private User FindReturnUser(int id)
        {
            using (BlogContext db = new BlogContext())
            {
                User user = db.Users.Find(id);

                return user;
            }
        }

        private void CheckField(UserViewModel model)
        {
            using (BlogContext db = new BlogContext())
            {
                
                if (db.Users.Count(u => u.ID != model.ID && u.Login == model.Login) > 0)
                {
                    ModelState.AddModelError("Login", "This login already exists");
                    return;
                }

                if (db.Users.Count(u => u.ID != model.ID && u.Email == model.Email) > 0)
                {
                    ModelState.AddModelError("Email", "This e-mail already exists");
                    return;
                }

            }

        }

        private User ModelToUser(UserViewModel model)
        {
            User user = new User();
            user.Age = model.Age;
            user.Name = model.Name;
            user.Login = model.Login;
            user.Surname = model.Surname;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ID = model.ID;
            user.CreationTime = model.CreationTime;
            
            return user;
        }

        private UserViewModel UserToModel(User user)
        {
            UserViewModel model = new UserViewModel();
            model.Name = user.Name;
            model.Age = user.Age;
            model.Email = user.Email;
            model.CreationTime = user.CreationTime;
            model.ID = user.ID;
            model.Login = user.Login;
            model.Password = user.Password;
            model.Surname = user.Surname;
            
            return model;
        }

        private PersonalPageViewModel UserToPersonalModel(User user)
        {
            PersonalPageViewModel model = new PersonalPageViewModel();
            model.Name = user.Name;
            model.Age = user.Age;
            model.Email = user.Email;
            model.CreationTime = user.CreationTime;
            model.ID = user.ID;
           
            return model;
        }

        private DeleteUserViewModel UserToDeleteModel(User user)
        {
            DeleteUserViewModel model = new DeleteUserViewModel();
            model.Name = user.Name;
            model.ID = user.ID;

            return model;
        }


        private IEnumerable<MyMessage> FindInputMessages(User user)
        {

            List<MyMessage> myInputMessages = new List<MyMessage>();
            
            using (BlogContext db = new BlogContext())
            {
                var temp = db.MessageToUsers
                    .Where(mtu => mtu.UserTo.ID == user.ID)
                    .ToList()   
                    .Select(mtu => new MyMessage(mtu))
                    .OrderByDescending(m=>m.CreationTime);
                
                myInputMessages.AddRange(temp);
            }
            return myInputMessages;

        }

        private IEnumerable<MyMessage> FindOutputMessages(User user)
        {
            List<MyMessage> myOutputMessages = new List<MyMessage>();
            
            using (BlogContext db = new BlogContext())
            {
                var temp = db.MessageToUsers
                    .Where(mtu => mtu.UserFrom.ID == user.ID)
                    .ToList()
                    .Select(mtu => new MyMessage(mtu))
                    .OrderByDescending(m=>m.CreationTime);

                myOutputMessages.AddRange(temp);
            }
            return myOutputMessages;
        }


    }
}
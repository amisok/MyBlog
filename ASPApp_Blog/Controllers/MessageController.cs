using ASPApp_Blog.Models;
using ASPApp_Blog.ViewModels;
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
        public ActionResult UsersList(int id = 0)
        {
            MessageUsersViewModel forMessage = new MessageUsersViewModel();
            

            using (BlogContext db = new BlogContext())
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }


                var temp = db.Users.Where(item => item.ID != user.ID)
                                   .Select(u => new UserForMessage { ID = u.ID,
                                                                     Name = u.Name,
                                                                     Surname = u.Surname});
                forMessage.SenderID = user.ID;
                forMessage.Users.AddRange(temp);
            }
            return View(forMessage);
        }
        
        [HttpGet]
        public ActionResult Create(int userFromID, int userToID)
        {
            CreateMessageViewModel model = new CreateMessageViewModel();
            using (BlogContext db = new BlogContext())
            {
                User userFrom = db.Users.Find(userFromID);
                User userTo = db.Users.Find(userToID);
                if (userFrom == null||userTo==null)
                {
                    return HttpNotFound();
                }
                
                model.UserFromID = userFrom.ID;
                model.UserToID = userTo.ID;
                model.UserToName = userTo.Name;
                model.UserToSurname = userTo.Surname;
                
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateMessage( int userFromID, int userToID, string text)
        {
            Message message = new Message();
            message.CreationTime = DateTime.Now;
            message.Text = text;
            MessageToUser messageToUser = new MessageToUser();
            using (BlogContext db = new BlogContext())
            {
                User sender = db.Users.Find(userFromID);
                User userTo = db.Users.Find(userToID);
                if (sender == null||userTo==null)
                {
                    return HttpNotFound();
                }
                
                db.Messages.Add(message);
                messageToUser.UserFrom = sender;
                messageToUser.UserTo = userTo;
                messageToUser.Message = message;
                db.MessageToUsers.Add(messageToUser);
                db.SaveChanges();

            }
            return RedirectToAction("PersonalPage", "Personal", new { id = userFromID });
        }
    }
}
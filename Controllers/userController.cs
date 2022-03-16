using Majal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Majal.Controllers
{
    public class userController : Controller
    {
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(userEdit user)
        {
            using (userEntities db = new userEntities())
            {
                var obj = db.users.FirstOrDefault(u => u.user_name.Equals(user.user_name) && u.password.Equals(user.password));
                if (obj != null)
                {
                    Session["userID"] = obj.user_id.ToString();
                    Session["userName"] = obj.user_name.ToString();
                    Session["password"] = obj.password.ToString();
                    Session["email"] = obj.email.ToString();
                    Session["phoneNo"] = obj.phoneNo.ToString();
                    return RedirectToAction("UserDashBoard");
                }
            }
            return View(user);
        }

        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signup(user user)
        {
            using (userEntities db = new userEntities())
            {
                var entity = db.users.FirstOrDefault(u => u.user_name.Equals(user.user_name) && u.password.Equals(user.password));
                if (entity == null && user != null)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    Session["userID"] = user.user_id.ToString();
                    Session["userName"] = user.user_name.ToString();
                    Session["password"] = user.password.ToString();
                    Session["email"] = user.email.ToString();
                    Session["phoneNo"] = user.phoneNo.ToString();
                    return RedirectToAction("UserDashBoard2");
                }
            }
            return View(user);
        }

        public ActionResult lockProfile()
        {
            using (userEntities db = new userEntities())
            {
                string temp = Session["userID"].ToString();
                int id = int.Parse(temp);
                var entity = db.users.FirstOrDefault(u => u.user_id == id);
                if (entity != null)
                {
                    using (appointmentEntities app = new appointmentEntities())
                    {
                        var appointments = app.appointments.Where(a => a.userid == id).ToList();
                        foreach (var x in appointments)
                        {
                            app.appointments.Remove(x);
                        }
                        app.SaveChanges();
                    }
                    db.users.Remove(entity);
                    db.SaveChanges();
                    return RedirectToAction("login");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? user_id)
        {
            using (userEntities db = new userEntities())
            {
                string temp = Session["userID"].ToString();
                user_id = int.Parse(temp);
                var entity = db.users.Where(u => u.user_id == user_id).FirstOrDefault();
                return View(entity);
            }
        }

        [HttpPost]
        public ActionResult Edit(user user)
        {
            using (userEntities db = new userEntities())
            {
                string temp = Session["userID"].ToString();
                user.user_id = int.Parse(temp);
                var entity = db.users.Where(u => u.user_id == user.user_id).FirstOrDefault();
                if (entity != null)
                {
                    entity.user_name = user.user_name;
                    entity.email = user.email;
                    entity.password = user.password;
                    entity.phoneNo = user.phoneNo;
                    db.SaveChanges();
                    Session["userName"] = entity.user_name.ToString();
                    Session["password"] = entity.password.ToString();
                    Session["email"] = entity.email.ToString();
                    Session["phoneNo"] = entity.phoneNo.ToString();
                    return RedirectToAction("UserDashBoard");
                }
                
                return View(user);
            }
        }

        public ActionResult UserDashBoard()
        {
            if(Session["userName"]!=null&& Session["password"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }

        public ActionResult UserDashBoard2()
        {
            if (Session["userName"] != null && Session["password"] != null
                && Session["email"] != null && Session["phoneNo"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("signup");
            }
        }
    }
}
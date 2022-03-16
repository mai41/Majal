using Majal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Majal.Controllers
{
    public class appointmentController : Controller
    {
        public ActionResult cancelReservedCourse()
        {
            return View();
        }

        public ActionResult fullCourse()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getAppList()
        {
            if (ModelState.IsValid)
            {
                List<course> courses = new List<course>();
                using (appointmentEntities entities = new appointmentEntities())
                {
                    string temp = Session["userID"].ToString();
                    int userId = int.Parse(temp);
                    var appointments = entities.appointments.Where(a => a.userid == userId).ToList();
                    foreach(appointment x in appointments)
                    {
                        using (courseEntities db = new courseEntities())
                        {
                            var cr = db.courses.FirstOrDefault(c => c.course_id == x.courseid);
                            courses.Add(cr);
                        }
                    }
                    return View(courses);
                }
            }
            return View();
        }


        public ActionResult cancelAppointment(appointment app)
        {
            using (appointmentEntities db = new appointmentEntities())
            {
                DateTime today = DateTime.Today;
                var entity = db.appointments.FirstOrDefault(a => a.courseid == app.courseid);
                if (entity != null)
                {
                    using (courseEntities cr = new courseEntities())
                    {
                        var course = cr.courses.FirstOrDefault(c => c.course_id == app.courseid);
                        if (course.startsFrom <= today)
                        {
                            return RedirectToAction("cancelReservedCourse");
                        }
                    }
                    db.appointments.Remove(entity);
                    db.SaveChanges();
                    return RedirectToAction("getAppList");
                }
            }
            return View(app);
        }
    }
}
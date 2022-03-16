using Majal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Majal.Controllers
{
    public class courseController : Controller
    {
        [HttpGet]
        public ActionResult getCreativeWritingList()
        {
            DateTime today = DateTime.Today;
            if (ModelState.IsValid)
            {
                using (courseEntities db = new courseEntities())
                {
                    var cw = db.courses.Where(c => c.couse_name.Equals("creative writing") && c.startsFrom > today).ToList();
                    return View(cw);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult getProgrammingList()
        {
            DateTime today = DateTime.Today;
            if (ModelState.IsValid)
            {
                using (courseEntities db = new courseEntities())
                {
                    var cw = db.courses.Where(c => c.couse_name.Equals("Programming") && c.startsFrom > today).ToList();
                    return View(cw);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult getMarketingList()
        {
            DateTime today = DateTime.Today;
            if (ModelState.IsValid)
            {
                using (courseEntities db = new courseEntities())
                {
                    var cw = db.courses.Where(c => c.couse_name.Equals("Marketing") && c.startsFrom > today).ToList();
                    return View(cw);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult getDesignList()
        {
            DateTime today = DateTime.Today;
            if (ModelState.IsValid)
            {
                using (courseEntities db = new courseEntities())
                {
                    var cw = db.courses.Where(c => c.couse_name.Equals("Designing") && c.startsFrom > today).ToList();
                    return View(cw);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult getEditingList()
        {
            DateTime today = DateTime.Today;
            if (ModelState.IsValid)
            {
                using (courseEntities db = new courseEntities())
                {
                    var cw = db.courses.Where(c => c.couse_name.Equals("Editing") && c.startsFrom > today).ToList();
                    return View(cw);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult getLanguageList()
        {
            DateTime today = DateTime.Today;
            if (ModelState.IsValid)
            {
                using (courseEntities db = new courseEntities())
                {
                    var cw = db.courses.Where(c => c.startsFrom > today && (c.couse_name.Equals("English") || c.couse_name.Equals("French") || c.couse_name.Equals("Germany") || c.couse_name.Equals("Spanish"))).ToList();
                    return View(cw);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult getCourses(string option, string search)
        {
            if (ModelState.IsValid)
            {
                using (courseEntities entities = new courseEntities())
                {
                    if (option != null)
                    {
                        if (option.Equals("Course Name"))
                        {
                            return View(entities.courses.Where(c => c.couse_name.Contains(search) || search == null).ToList());
                        }
                        if (option.Equals("Instuctor ID"))
                        {
                            int s = int.Parse(search);
                            return View(entities.courses.Where(c => c.Instructor == s || search == null).ToList());
                        }
                        if (option.Equals("Day Of Course"))
                        {
                            return View(entities.courses.Where(c => c.Day.Contains(search) || search == null).ToList());
                        }
                    }
                    var courses = entities.courses.ToList();

                    return View(courses);
                }
            }
            return View();
        }

        public ActionResult addCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addCourse(course course)
        {
            using (courseEntities db = new courseEntities())
            {
                DateTime today = DateTime.Today;
                List<string> crNames = new List<string>();
                List<int> instructors = new List<int>();
                using (instructorEntities ins = new instructorEntities())
                {
                    var list = ins.instructors.ToList();
                    foreach (instructor i in list)
                    {
                        if (!(crNames.Contains(i.course)))
                        {
                            crNames.Add(i.course);
                        }
                        if (!(instructors.Contains(i.id)))
                        {
                            instructors.Add(i.id);
                        }
                    }
                }
                var entity = db.courses.FirstOrDefault(c => c.Instructor == course.Instructor && c.Day.Equals(course.Day) && c.StartSlot == course.StartSlot && c.EndSlot == course.EndSlot);
                if (ModelState.IsValid)
                {
                    if (course != null && crNames.Contains(course.couse_name) && instructors.Contains(course.Instructor)
                        && course.startsFrom > today)
                    {
                        if (entity != null)
                        {
                            return RedirectToAction("getCourses");
                        }
                        db.courses.Add(course);
                        db.SaveChanges();
                        return RedirectToAction("getCourses");
                    }
                        
                }
            }
            return View(course);
        }

        [HttpGet]
        public ActionResult editCourse(int? course_id)
        {
            using (courseEntities db = new courseEntities())
            {
                var entity = db.courses.Where(u => u.course_id == course_id).FirstOrDefault();
                Session["courseID"] = course_id.ToString();
                return View(entity);
            }
        }

        [HttpPost]
        public ActionResult editCourse(course course)
        {
            using (courseEntities db = new courseEntities())
            {
                DateTime today = DateTime.Today;
                List<string> crNames = new List<string>();
                List<int> instructors = new List<int>();
                using (instructorEntities ins = new instructorEntities())
                {
                    var list = ins.instructors.ToList();
                    foreach(instructor i in list)
                    {
                        if (!(crNames.Contains(i.course)))
                        {
                            crNames.Add(i.course);
                        }
                        if (!(instructors.Contains(i.id)))
                        {
                            instructors.Add(i.id);
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    string temp = Session["courseID"].ToString();
                    course.course_id = int.Parse(temp);
                    var entity = db.courses.Where(u => u.course_id == course.course_id).FirstOrDefault();
                    if (entity != null && crNames.Contains(course.couse_name) && instructors.Contains(course.Instructor)
                        && course.startsFrom > today)
                    {
                        entity.couse_name = course.couse_name;
                        entity.Instructor = course.Instructor;
                        entity.Day = course.Day;
                        entity.StartSlot = course.StartSlot;
                        entity.EndSlot = course.EndSlot;
                        entity.startsFrom = course.startsFrom;
                        entity.Duration = course.Duration;
                        entity.hallNo = course.hallNo;
                        entity.Capacity = course.Capacity;
                        db.SaveChanges();
                        return RedirectToAction("getCourses");
                    }
                }
            }
            return View(course);
        }

        public ActionResult deleteCourse(course course)
        {
            using (courseEntities db = new courseEntities())
            {
                DateTime today = DateTime.Today;
                var entity = db.courses.FirstOrDefault(c => c.course_id == course.course_id);
                if (entity != null && entity.startsFrom > today)
                {
                    db.courses.Remove(entity);
                    db.SaveChanges();
                    return RedirectToAction("getCourses");
                }
            }
            return View(course);
        }

        public ActionResult ReseveApp(appointment app)
        {
            using (appointmentEntities db = new appointmentEntities())
            {
                if (ModelState.IsValid)
                {
                    string temp = Session["userID"].ToString();
                    int userId = int.Parse(temp);
                    var entity = db.appointments.FirstOrDefault(a => a.userid == userId && a.courseid == app.courseid);
                    var appList = db.appointments.Where(a => a.courseid == app.courseid).ToList();
                    course course = new course();
                    using (courseEntities cr = new courseEntities())
                    {
                        course = cr.courses.FirstOrDefault(c => c.course_id == app.courseid);
                    }
                    if (entity != null)
                    {
                        return RedirectToAction("UserDashBoard", "user");
                    }
                    if (course.Capacity <= appList.Count())
                    {
                        return RedirectToAction("fullCourse","appointment");
                    }
                    app.userid = userId;
                    db.appointments.Add(app);
                    db.SaveChanges();
                    return RedirectToAction("UserDashBoard","user");
                }
            }
            return View(app);
        }

    }
}
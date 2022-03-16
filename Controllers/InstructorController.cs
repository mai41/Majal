using Majal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Majal.Controllers
{
    public class InstructorController : Controller
    {
        [HttpGet]
        public ActionResult getInstructorsList(string option, string search)
        {
            if (ModelState.IsValid)
            {
                using (instructorEntities entities = new instructorEntities())
                {
                    if (option != null)
                    {
                        if (option.Equals("Instructor Name"))
                        {
                            return View(entities.instructors.Where(i => i.Name.Contains(search) || search == null).ToList());
                        }
                        if (option.Equals("Course"))
                        {
                            return View(entities.instructors.Where(i => i.course.Contains(search) || search == null).ToList());
                        }
                    }
                    var instructors = entities.instructors.ToList();
                    
                    return View(instructors);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult getInstructors(string option, string search)
        {
            if (ModelState.IsValid)
            {
                using (instructorEntities entities = new instructorEntities())
                {
                    if (option != null)
                    {
                        if (option.Equals("Instructor Name"))
                        {
                            return View(entities.instructors.Where(i => i.Name.Contains(search) || search == null).ToList());
                        }
                        if (option.Equals("Course"))
                        {
                            return View(entities.instructors.Where(i => i.course.Contains(search) || search == null).ToList());
                        }
                    }
                    var instructors = entities.instructors.ToList();

                    return View(instructors);
                }
            }
            return View();
        }

        public ActionResult addIns()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addIns(instructor ins)
        {
            using (instructorEntities db = new instructorEntities())
            {
                if (ModelState.IsValid)
                {
                    var entity = db.instructors.FirstOrDefault(i => i.Name.Equals(ins.Name) && i.course.Equals(ins.course) && i.Description.Equals(ins.Description));
                    if (entity != null)
                    {
                        return RedirectToAction("getInstructors");
                    }
                    db.instructors.Add(ins);
                    db.SaveChanges();
                    return RedirectToAction("getInstructors");
                }
            }
            return View(ins);
        }

        [HttpGet]
        public ActionResult editIns(int? id)
        {
            using (instructorEntities db = new instructorEntities())
            {
                var entity = db.instructors.Where(i => i.id == id).FirstOrDefault();
                Session["InsID"] = id.ToString();
                return View(entity);
            }
        }

        [HttpPost]
        public ActionResult editIns(instructor ins)
        {
            using (instructorEntities db = new instructorEntities())
            {
                if (ModelState.IsValid)
                {
                    string temp = Session["InsID"].ToString();
                    ins.id = int.Parse(temp);
                    var entity = db.instructors.FirstOrDefault(i => i.id == ins.id);
                    if (entity != null)
                    {
                        entity.Name = ins.Name;
                        entity.course = ins.course;
                        entity.Description = ins.Description;
                        db.SaveChanges();
                        return RedirectToAction("getInstructors");
                    }
                }
            }
            return View(ins);
        }

        public ActionResult deleteIns(instructor ins)
        {
            using (instructorEntities db = new instructorEntities())
            {
                var entity = db.instructors.FirstOrDefault(i => i.id == ins.id);
                if (entity != null)
                {
                    db.instructors.Remove(entity);
                    db.SaveChanges();
                    return RedirectToAction("getInstructors");
                }
            }
            return View(ins);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Majal.Models;

namespace Majal.validationAttributes
{
    public class specifiedCourses : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<string> crNames = new List<string>();
            using (instructorEntities ins = new instructorEntities())
            {
                var list = ins.instructors.ToList();
                foreach (instructor i in list)
                {
                    if (!(crNames.Contains(i.course)))
                    {
                        crNames.Add(i.course);
                    }
                }
            }

            if (value is string)
            {
                var v = value as string;
                if (crNames.Contains(v))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
               
    }
}
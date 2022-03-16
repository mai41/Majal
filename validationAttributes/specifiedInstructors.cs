using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Majal.Models;

namespace Majal.validationAttributes
{
    public class specifiedInstructors : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<int?> instructors = new List<int?>();
            using (instructorEntities ins = new instructorEntities())
            {
                var list = ins.instructors.ToList();
                foreach (instructor i in list)
                {
                    if (!(instructors.Contains(i.id)))
                    {
                        instructors.Add(i.id);
                    }
                }
            }

            if (value is int?)
            {
                var v = value as int?;
                if (instructors.Contains(v))
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
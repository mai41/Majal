using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Majal.Models;

namespace Majal.validationAttributes
{
    public class inFuture : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime?)
            {
                var v = value as DateTime?;
                if (v.Value > DateTime.Today)
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
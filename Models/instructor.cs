//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Majal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class instructor
    {
        [Display(Name ="Instructor ID")]
        public int id { get; set; }
        [Display(Name = "Instructor Name")]
        public string Name { get; set; }
        [Display(Name = "Course")]
        public string course { get; set; }
        [Display(Name = "Certificates & Achievements")]
        public string Description { get; set; }
    }
}

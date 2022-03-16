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
    using Majal.validationAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class course
    {
        [Required]
        [Display(Name ="Course ID")]
        public int course_id { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        [specifiedCourses(ErrorMessage ="You Entered Wrong Course Name")]
        public string couse_name { get; set; }
        [Required]
        [Display(Name = "Instuctor ID")]
        [specifiedInstructors(ErrorMessage = "You Entered Wrong Instructor ID")]
        public int Instructor { get; set; }
        [Required]
        [Display(Name = "Day Of Course")]
        public string Day { get; set; }
        [Required]
        [Display(Name = "Starts At")]
        public int StartSlot { get; set; }
        [Required]
        [Display(Name = "Ends At")]
        public int EndSlot { get; set; }
        [Required]
        [Display(Name = "Starting Day")]
        [DataType(DataType.Date)]
        [inFuture(ErrorMessage ="Please Enter a Future Date")]
        public System.DateTime startsFrom { get; set; }
        [Required]
        [Display(Name = "Duration")]
        public string Duration { get; set; }
        [Required]
        [Display(Name = "Hall Number")]
        public int hallNo { get; set; }
        [Required]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }
    }
}
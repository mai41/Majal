﻿namespace Majal.Models
{
    using Majal.validationAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class adminEdit : Admin
    {
        [IncorrectUserNameOrPassword(ErrorMessage = "Incorrect User Name or Password")]
        public string admin_name { get; set; }
        [IncorrectUserNameOrPassword(ErrorMessage = "Incorrect User Name or Password")]
        public string password { get; set; }
    }
}
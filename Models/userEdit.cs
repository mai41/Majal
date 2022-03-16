

namespace Majal.Models
{
    using Majal.validationAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class userEdit : user
    {
        [IncorrectUserNameOrPassword(ErrorMessage = "Incorrect User Name or Password")]
        public string user_name { get; set; }
        [IncorrectUserNameOrPassword(ErrorMessage = "Incorrect User Name or Password")]
        public string password { get; set; }
    }
}
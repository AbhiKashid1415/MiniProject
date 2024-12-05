using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMini.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email Id is required")]
        [EmailAddress(ErrorMessage = "Email Id is Invalid")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
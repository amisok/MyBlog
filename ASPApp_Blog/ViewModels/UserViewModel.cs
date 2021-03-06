﻿using ASPApp_Blog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPApp_Blog.ViewModels
{
    public class UserViewModel
    {

        public int ID { get; set; }

        [Required]
        [Remote("ValidateLogin", "Personal", AdditionalFields = "ID")]
        public string Login { get; set; }
       
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "The length must be from 5 to 20 symbols")]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords are different")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public Nullable<int> Age { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Uncorrect adress")]
        [Remote("ValidateEmail","Personal",AdditionalFields ="ID")]
        public string Email { get; set; }

        public System.DateTime CreationTime { get; set; }
    }
}
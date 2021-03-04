using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MySchoolApp;

namespace MySchoolApp.Model
{
    public class User
    {

        [Required]
        public int ID { get; set; }

        [Required]
        public string NISN { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required] 
        public int Balance { get; set; }
    }
}

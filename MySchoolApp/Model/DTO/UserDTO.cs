using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MySchoolApp.Model.DTO
{
    public class UserDTO
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Required, Unique]
        public string Email { get; set; }

        [Required]
        public DateTime LastLogin { get; set; }

    }
}

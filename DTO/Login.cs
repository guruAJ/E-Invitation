﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.DTO
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        public int TypeId { get; set; }
        [Column(TypeName = "varchar(200)")]
        
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
      
        public int GroupId { get; set; }
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoencoPOS.Models
{
    public class Register
    {
        public string Id { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
        public string Role { get; set; }
        [Required]
        [DisplayName("Branch")]
        public int BranchId { get; set; }
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.User
{
public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string? UserName{ get; set; }
        [Required]
        [Display(Name = "Ad Soyad")]
        public string? Name{ get; set; }
        [Required]
        [EmailAddress]
        [Display(Name ="Eposta")]
        public string? Email { get; set; }
        [Required]
        [StringLength(10,ErrorMessage ="{0} alani en az {2} karakter uzunlugunda olmalidir",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="{0} alani en az {2} karakter uzunlugunda olmalidir",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Parolaniz eslesmiyor")]
        [Display(Name ="Parola tekrar")]
        public string? ConfirmPassword { get; set; }
    }
}
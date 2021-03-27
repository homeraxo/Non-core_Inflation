using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Non_core_Inflation.Models
{
    public class AccountViewModels
    {
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ehebruch.Models.Account
{
    public class LogViewModel
    {
        [Required(ErrorMessage = "Неверный адрес почты")]
        [Display(Name = "E-mail: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Неверный пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
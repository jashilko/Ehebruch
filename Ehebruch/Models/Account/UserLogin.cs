using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ehebruch.Models.Account;


namespace Ehebruch.Models
{
    // Класс 
    public class UserLogin   
    {
        // ИД 
        [Required]
        [ScaffoldColumn(false)]
        public int id { get; set; }

        [Required(ErrorMessage="Ник должен быть задан")]
        [StringLength (50, MinimumLength=3,ErrorMessage="Длина строки должна быть от 3 до 30 символов")]
        [Display(Name = "Ник")]
        public string nic { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string pass { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Электронная почта")]
        public string email { get; set; }

        [Display(Name = "Подтверждение")]
        public short? confirm { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        [Display(Name = "Электронная почта")]
        public DateTime? CreationDate { get; set; }


    }


}
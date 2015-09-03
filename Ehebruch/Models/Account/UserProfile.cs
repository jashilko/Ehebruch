using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ehebruch.Models.Account
{
    // Анкета пользователя. 
    public class UserProfile
    {
        // ИД 
        [Required]
        [ScaffoldColumn(false)]
        public int id { get; set; }
    
        [Required]
        [ScaffoldColumn(false)]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime dateOfBirthday { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public string country { get; set; }

        [Required]
        [Display(Name = "Регион")]
        public string region { get; set; }

        [Required]
        [Display(Name = "Город")]
        public string sity { get; set; }

        // Желание - битовая маска. 
        [Display(Name = "Желание")]
        public short? wish { get; set; }

        [Display(Name = "Рост")]
        [Range(50, 250, ErrorMessage = "Странный у вас рост")]
        public short? height { get; set; }

        [Display(Name = "Вес")]
        [Range(30, 250, ErrorMessage = "Странный у вас вес")]
        public short? weight { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public bool sex { get; set; }

        [Display(Name = "Дата последнего обновления")]
        [ScaffoldColumn(false)]
        public DateTime? LastUpdateDate { get; set; }

        [Display(Name = "Аватар")]
        [ScaffoldColumn(false)]
        public string AvatarPath { get; set; } 



    }

}
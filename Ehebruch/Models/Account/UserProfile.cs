using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ehebruch.Models
{
    // Анкета пользователя. 
    public class UserProfile
    {
        // ИД 
        [Required]
        [ScaffoldColumn(false)]
        public int id { get; set; }
    
        [Required]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? dateOfBirthday;

        [Required]
        [Display(Name = "Страна")]
        public string country;

        [Required]
        [Display(Name = "Регион")]
        public string region;

        [Required]
        [Display(Name = "Город")]
        public string sity;

        // Желание - битовая маска. 
        [Display(Name = "Желание")]
        public int wish;

        [Display(Name = "Рост")]
        [Range(50, 250, ErrorMessage = "Странный у вас рост")]
        public short height;

        [Display(Name = "Вес")]
        [Range(30, 250, ErrorMessage = "Странный у вас вес")]
        public short weight;

        [Required]
        [Display(Name = "Пол")]
        public bool sex;

    }

}
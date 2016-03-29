using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ehebruch.Models.Account
{
    public class UserFoto
    {
        // ИД 
        [Required]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Путь к файлу")]
        public string Path { get; set; }

        [Display(Name = "Описание")]
        public string Descript { get; set; }

        [Required]
        public DateTime? UploadDate { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    
    }
}
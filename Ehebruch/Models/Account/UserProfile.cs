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
        public int Id { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public int UserLoginId { get; set; }
        public virtual UserLogin UserLogin { get; set; }

        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? dateOfBirthday { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        [Required]
        [Display(Name = "Регион")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Required]
        [Display(Name = "Город")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

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

        public virtual IEnumerable<UserFoto> Fotoes { get; set; }

        [Display(Name = "Возраст")]
        [Range(18, 250, ErrorMessage = "Вы слишком молоды")]
        public byte? Age { get; set; }

        [Display(Name = "Телосложение")]
        public int? FigureId { get; set; }
        public virtual Figure Figure { get; set; }

        [Display(Name = "Курение")]
        public int? SmokingId { get; set; }
        public virtual Smoking Smokink { get; set; }

        [Display(Name = "Алкоголь")]
        public int? AlcoholId { get; set; }
        public virtual Alcohol Alcohol { get; set; }

        [Display(Name = "О себе")]
        public String About { get; set; }

        [Display(Name = "Мой идеал")]
        public String MyIdeal { get; set; }

        // Список языков. 
        [Display(Name = "Языки")]
        public virtual ICollection<Language> Languages { get; set; }
        
        // Что возбуждает. 
        [Display(Name = "Что меня возбуждает")]
        public virtual ICollection<Excitement> Excitements { get; set; }

        // Что видеть в партрене. 
        [Display(Name = "Что важно в партнере")]
        public virtual ICollection<Whatpartner> Whatpartners { get; set; }


        
        public UserProfile()
        {
            Languages = new List<Language>();
            Excitements = new List<Excitement>();
            Whatpartners = new List<Whatpartner>();
        }

    }

    // Телосложение. 
    public class Figure
    {
        public int Id { get; set; }
        public String Fig { get; set; }
    }

    // Курение
    public class Smoking
    {
        public int Id { get; set; }
        public String Smoke { get; set; }
    }

    // Употребление алкоголя.
    public class Alcohol
    {
        public int Id { get; set; }
        public String Alco { get; set; }
    }

    // Языки
    public class Language
    {
        public int Id { get; set; }
        public String Lang { get; set; }

        // Связь много-много
        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        public Language()
        {
            UserProfiles = new List<UserProfile>();
        }
    }

    // Что возбуждает. 
    public class Excitement
    {
        public int Id { get; set; }
        // Что именно возбуждает. 
        public String Excit { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public Excitement()
        {
            UserProfiles = new List<UserProfile>();
        }

    }

    // Какой партнер
    public class Whatpartner
    {
        public int Id { get; set; }

        public String Whatpart { get; set; }
        public Boolean? Smb { get; set; }


        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public Whatpartner()
        {
            UserProfiles = new List<UserProfile>();
        }
    }
}
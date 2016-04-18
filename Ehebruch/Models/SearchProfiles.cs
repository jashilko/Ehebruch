using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ehebruch.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Ehebruch.Models
{
    public class SearchProfiles
    {
        public short? AgeFrom { get; set; }
        public short? AgeTo { get; set; }

        [Display(Name = "Страна")]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        [Required]
        [Display(Name = "Регион")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        [Required]
        [Display(Name = "Город")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }
        

        public short? heightFrom { get; set; }
        public short? heightTo { get; set; }

        public short? weightFrom { get; set; }
        public short? weightTo { get; set; }

        public IEnumerable<UserProfile> Profiles { get; set; }

        public String SearchButton { get; set; }

        public Boolean? SearchSex { get; set; }

        public short? wish { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ehebruch.Models.Account;

namespace Ehebruch.Models
{
    public class SearchProfiles
    {
        public short? AgeFrom { get; set; }
        public short? AgeTo { get; set; }

        public String Sity { get; set; }

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
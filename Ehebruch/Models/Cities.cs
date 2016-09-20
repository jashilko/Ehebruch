using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ehebruch.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Priority { get; set; }
    }

    public class Region 
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public byte Priority { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public byte Priority { get; set; }
    }
}
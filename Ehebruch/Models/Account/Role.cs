using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ehebruch.Models
{

    public class Role
    {
        public int Id { get; set; }
        
        [Display(Name = "Статус")]
        public string Name { get; set; }
    }
}
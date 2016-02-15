﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ehebruch.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Ehebruch.Models
{
    public class Message
    {
        // ИД 
        [Required]
        [ScaffoldColumn(false)]
        public int id { get; set; }

        public int SenderId { get; set; }
        public UserProfile Sender { get; set; }

        public int RecipientId { get; set; }
        public UserProfile Recipient { get; set; }

        public String TextMessage { get; set; }

        public DateTime CreatedTime { get; set; }
    }

    public class Dialog
    {
        public int IdPerson { get; set; }
        public String TextMessage { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ehebruch.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace Ehebruch.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public virtual UserProfile Sender { get; set; }

        public int RecipientId { get; set; }
        public virtual UserProfile Recipient { get; set; }

        public String TextMessage { get; set; }

        public DateTime CreatedTime { get; set; }
        public bool? Read { get; set; }
    }

    public class Dialog
    {
        
        public int IdPerson { get; set; }
        public String nic { get; set; }

        public String AvatarPath { get; set; }
        public String TextMessage { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Ehebruch.Models
{
    public class EhebruchContex : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Account.UserProfile> UserProfiles { get; set; }
        public DbSet<Account.UserFoto> UserFotoes { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
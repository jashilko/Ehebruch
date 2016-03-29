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

        // Города
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }

        // Дополнения к профилю. 
        public DbSet<Account.Figure> Figures { get; set; }
        public DbSet<Account.Smoking> Smokings { get; set; }
        public DbSet<Account.Alcohol> Alcohols { get; set; }
        public DbSet<Account.Language> Languages { get; set; }
        public DbSet<Account.Excitement> Excitements { get; set; }
        public DbSet<Account.Whatpartner> Whatpartners { get; set; }


    }





}
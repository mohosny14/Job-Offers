﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Jop_Offers_Website.Models;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //  دى خاصه بالجدول بتاع الداتا بيزproperty ال
        public string UserType { get; set; }

        public virtual  ICollection<Job> jobs { get; set; } // object from class job: represent many relationship
        // one user to many jobs 

        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
                                               // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
              var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.ApplyForJob> ApplyForJobs { get; set; }

    }
}
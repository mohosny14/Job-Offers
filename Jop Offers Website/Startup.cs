using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefultRolesAndUsers();
        }
        // method to create Roles , Users and Add users to role
        public void CreateDefultRolesAndUsers()
        {
            // creat Role for manage Roles
            //roleStore لازم تعرف الداتا بيز لل 
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            // creat Role for manage Users
            var roleUser = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            IdentityRole role = new IdentityRole(); // this object to create the role
            if (!roleManger.RoleExists("Admins")) //نتأكد ان مفيش روول موجوده بالاسم دا
            {
                // create role
                role.Name = "Admins";
                roleManger.Create(role);

                // create User
                ApplicationUser user1 = new ApplicationUser();
                user1.UserName = "Mohamed";
                user1.Email = "mohosny14@gmail.com";
                var check = roleUser.Create(user1, "mo..hamedAli14");

                if (check.Succeeded)
                {
                    roleUser.AddToRole(user1.Id, "Admins");
                }
            }
        }
    }
}

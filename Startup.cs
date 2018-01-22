using jobaffair.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jobaffair.Startup))]
namespace jobaffair
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DefaultRolesAndUsers();
        }

        public void DefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admins"))
            {

                role.Name = "Admins";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "khalid";
                user.Email = "khalid@hotmail.com";
                var check = userManager.Create(user, "asD@123");
                if (check.Succeeded)
                {

                    userManager.AddToRole(user.Id, "Admins");

                }

            }








        }






    }
}

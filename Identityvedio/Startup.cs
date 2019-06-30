using System;
using Identityvedio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Identityvedio.Startup))]
namespace Identityvedio
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoles();
            app.MapSignalR();
        }

        private void createRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if(!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Freelancer"))
            {
                role = new IdentityRole();
                role.Name = "Freelancer";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Client"))
            {
                role = new IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);
            }
        }
    }
}

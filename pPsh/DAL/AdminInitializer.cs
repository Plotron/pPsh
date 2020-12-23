using pPsh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace pPsh.DAL
{
    public class AdminInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);

            var applicationDatabaseContext = context;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(applicationDatabaseContext));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(applicationDatabaseContext));

            roleManager.Create(new IdentityRole("admin"));
            roleManager.Create(new IdentityRole("mod"));
            roleManager.Create(new IdentityRole("user"));
            /*
            var users = new List<ApplicationUser>
            {
                new ApplicationUser {UserName = "admin@admin.com"},
                new ApplicationUser {UserName = "user@user.com"},
                new ApplicationUser {UserName = "mod@mod.com"},
            };

            users.ForEach(u =>
            {
                var prefix = u.UserName.Split('@')[0];

                userManager.Create(u, prefix);
                userManager.AddToRole(u.Id, prefix);
                userManager.AddToRole(u.Id, "user");
            });

            var profiles = new List<Profile>();
            users.ForEach(u => profiles.Add(new Profile(u.UserName)));

            context.Profiles.AddRange(profiles);

            var Scenarios = new List<Scenario>
            {
                new Scenario {ID = 1, Profile = profiles[0]},
                new Scenario {ID = 2, Profile = profiles[1]},
                new Scenario {ID = 3, Profile = profiles[2]},
            };

            var Devices = new List<Device>
            {
                new Device {ID = 1, Name = "Kitchen", Key = "TotallySecret", Enabled = true, Profile = profiles[0]},
                new Device {ID = 2, Name = "Garage", Key = "Garagedoor", Enabled = true, Profile = profiles[1]},
                new Device {ID = 3, Name = "Bedroom", Key = "BedroomSikrits", Enabled = false, Profile = profiles[2]},
            };

            context.Devices.AddRange(Devices);
            context.Scenarios.AddRange(Scenarios);
            */

            context.SaveChanges();
        }

    }
}
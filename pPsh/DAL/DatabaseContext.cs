using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace pPsh.Models
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
        }
        public DbSet<Profile> Profiles {get; set;}
        public DbSet<Device> Devices { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<DeviceEvent> DeviceEvents { get; set; }
        public DbSet<ScenarioEmail> ScenarioEmails { get; set; }
        
        public static DatabaseContext Create() {
            return new DatabaseContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                .HasRequired(d => d.Profile)
                .WithMany(p => p.Devices)
                .HasForeignKey(d => d.ProfileID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeviceEvent>()
                .Property(e => e._Parameters).HasColumnName("Parameters");

        }

    }

}
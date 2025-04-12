using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Data
{
    public class ShippingContext : IdentityDbContext<AppUser>
    {
        public ShippingContext(DbContextOptions<ShippingContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Ensure default Governorate exists
            modelBuilder.Entity<Governorate>().HasData(
                new Governorate { Id = 1, Name = "Default Governorate" }
            );

            // Ensure default City exists (with a valid GovernorateId)
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Default City", GovernorateId = 1 }
            );

            // Ensure default Area exists (with a valid CityId)
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Default Area", CityId = 1, IsActive = true }
            );

        }

        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserGroupPermission> UserGroupPermissions { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTracking> OrderTrackings { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ShippingType> ShippingTypes { get; set; }
        public DbSet<UserBranch> UserBranches { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WeightSetting> WeightSettings { get; set; }



    }
}

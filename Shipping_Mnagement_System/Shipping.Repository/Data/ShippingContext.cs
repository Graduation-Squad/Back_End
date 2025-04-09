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



    }
}

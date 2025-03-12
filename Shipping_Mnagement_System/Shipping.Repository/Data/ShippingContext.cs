using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Models;
using Shipping.Core.Models.Identity;
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

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
    }
}

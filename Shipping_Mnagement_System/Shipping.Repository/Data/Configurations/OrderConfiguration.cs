using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           
            builder.Property(o => o.TotalWeight).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(o => o.ShippingCost).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(o => o.CODAmount).HasColumnType("decimal(18,2)").IsRequired();

            // Relationships
            builder.HasOne(o => o.Merchant)
                   .WithMany(u => u.Merchant.CreatedOrders)
                   .HasForeignKey(o => o.MerchantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Area)
                   .WithMany(a => a.Orders)
                   .HasForeignKey(o => o.AreaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.City)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CityId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Governorate)
                   .WithMany(g => g.Orders)
                   .HasForeignKey(o => o.GovernorateId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.PaymentMethod)
                   .WithMany(pm => pm.Orders)
                   .HasForeignKey(o => o.PaymentMethodId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ShippingType)
                   .WithMany()
                   .HasForeignKey(o => o.ShippingTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.CreatedBy)
                   .WithMany()
                   .HasForeignKey(o => o.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.DeliveryAgent)
                   .WithMany(u => u.DeliveryMan.AssignedOrders)
                   .HasForeignKey(o => o.DeliveryAgentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Branch)
                   .WithMany(b => b.Orders)
                   .HasForeignKey(o => o.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

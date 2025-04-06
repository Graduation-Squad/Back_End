using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Data.Configurations
{
    public class OrderTrackingConfiguration : IEntityTypeConfiguration<OrderTracking>
    {
        public void Configure(EntityTypeBuilder<OrderTracking> builder)
        {
            builder.Property(ot => ot.Status).HasConversion<string>().IsRequired();

            // Relationships
            builder.HasOne(ot => ot.Order)
                   .WithMany(o => o.OrderTrackings)
                   .HasForeignKey(ot => ot.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ot => ot.RejectionReason)
                   .WithMany()
                   .HasForeignKey(ot => ot.RejectionReasonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ot => ot.User)
                   .WithMany(u => u.OrderTrackings)
                   .HasForeignKey(ot => ot.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

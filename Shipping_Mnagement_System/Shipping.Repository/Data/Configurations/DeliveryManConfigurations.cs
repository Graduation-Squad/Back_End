using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Data.Configurations
{
    public class DeliveryManConfigurations : IEntityTypeConfiguration<DeliveryMan>
    {

        public void Configure(EntityTypeBuilder<DeliveryMan> builder)
        {
            builder.Property(m => m.DiscountValue).HasColumnType("decimal(18,2)");
            builder.Property(m => m.DiscountType).HasConversion<string>();

            builder.HasOne(d => d.AppUser)
                .WithOne(a => a.DeliveryMan)
                .HasForeignKey<DeliveryMan>(d => d.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

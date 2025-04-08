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
    public class MerchantConfigurations : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.Property(m => m.PickupCost).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(m => m.RejectedOrdersShippingRatio).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(m => m.AppUser)
                .WithOne(a => a.Merchant)
                .HasForeignKey<Merchant>(m => m.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

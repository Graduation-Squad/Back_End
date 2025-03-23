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
    public class WeightSettingConfiguration : IEntityTypeConfiguration<WeightSetting>
    {
        public void Configure(EntityTypeBuilder<WeightSetting> builder)
        {
            builder.Property(ws => ws.BaseWeight).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(ws => ws.AdditionalWeightPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(ws => ws.BaseWeightPrice).HasColumnType("decimal(18,2)").IsRequired();
         
            builder.HasOne(ws => ws.Governorate)
                   .WithMany(g => g.WeightSettings)
                   .HasForeignKey(ws => ws.GovernorateId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

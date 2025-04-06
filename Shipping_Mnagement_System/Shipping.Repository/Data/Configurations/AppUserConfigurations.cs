using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Data.Configurations
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.UpdatedAt).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.UserType).IsRequired().HasConversion<string>();

            builder.HasOne(u => u.UserGroup)
                   .WithMany(ug => ug.Users)
                   .HasForeignKey(u => u.UserGroupId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

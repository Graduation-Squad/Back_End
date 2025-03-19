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
    public class UserGroupPermissionConfiguration : IEntityTypeConfiguration<UserGroupPermission>
    {
        public void Configure(EntityTypeBuilder<UserGroupPermission> builder)
        {
            builder.HasKey(ugp => new { ugp.UserGroupId, ugp.PermissionId });

            builder.HasOne(ugp => ugp.UserGroup)
                   .WithMany(ug => ug.UserGroupPermissions)
                   .HasForeignKey(ugp => ugp.UserGroupId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ugp => ugp.Permission)
                   .WithMany(p => p.UserGroupPermissions)
                   .HasForeignKey(ugp => ugp.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

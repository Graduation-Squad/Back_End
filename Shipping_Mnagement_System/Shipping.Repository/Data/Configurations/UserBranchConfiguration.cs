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
    public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
    {
        public void Configure(EntityTypeBuilder<UserBranch> builder)
        {
            builder.HasKey(ub => new { ub.UserId, ub.BranchId });

            builder.HasOne(ub => ub.User)
                   .WithMany(u => u.UserBranches)
                   .HasForeignKey(ub => ub.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ub => ub.Branch)
                   .WithMany(b => b.UserBranches)
                   .HasForeignKey(ub => ub.BranchId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

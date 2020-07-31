using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesMap
{
    public class VendorUserMap : IEntityTypeConfiguration<VendorUser>
    {
        public void Configure(EntityTypeBuilder<VendorUser> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Phone).IsRequired();         
            builder.Property(t => t.PasswordHash).IsRequired();
            builder.Property(t => t.PasswordSalt).IsRequired();
            builder.Property(t => t.IsRandom).HasDefaultValue(false);
            builder.Property(t => t.Role).HasDefaultValue(VendorUserRoleEnum.Admin);
            builder.HasOne(h => h.Vendor).WithMany(w => w.VendorUsers).HasForeignKey(h => h.VendorId);
        }
    }
}

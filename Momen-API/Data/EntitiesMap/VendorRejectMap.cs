using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.EntitiesMap
{
    public class VendorRejectMap : IEntityTypeConfiguration<VendorReject>
    {
        public void Configure(EntityTypeBuilder<VendorReject> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Reason).IsRequired();
            builder.Property(t => t.CreationDate).HasDefaultValue(DateTime.Now);
            builder.HasOne(h => h.Vendor).WithMany(w => w.VendorRejects).HasForeignKey(h => h.VendorId);
        }
    }
}

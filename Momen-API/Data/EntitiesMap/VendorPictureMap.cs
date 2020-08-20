using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesMap
{
    public class VendorPictureMap : IEntityTypeConfiguration<VendorPicture>
    {
        public void Configure(EntityTypeBuilder<VendorPicture> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.PictureName).IsRequired();
            builder.HasOne(h => h.Vendor).WithMany(w => w.VendorPictures).HasForeignKey(h => h.VendorId);
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntitiesMap
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Phone).IsRequired();
            builder.Property(t => t.PasswordHash).IsRequired();
            builder.Property(t => t.PasswordSalt).IsRequired();
            builder.Property(t => t.IsRandom).HasDefaultValue(true);
            builder.Property(t => t.IsBlocked).HasDefaultValue(false);
        }
    }
}

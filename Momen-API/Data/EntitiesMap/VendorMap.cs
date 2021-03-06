﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.EntitiesMap
{
    public class VendorMap : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.BranchesCount).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.CategoryId).IsRequired();
            builder.Property(t => t.Status).IsRequired();
            builder.Property(t => t.CreationDate).HasColumnType("date").HasDefaultValue(DateTime.Now);
            builder.HasOne(h => h.Category).WithMany(w => w.Vendors).HasForeignKey(h => h.CategoryId);
            builder.HasOne(h => h.Contract).WithMany(w => w.Vendors).HasForeignKey(h => h.ContractId);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class Package : AuditableEntity
{
    public string Name { get; set; }
    public int Uses { get; set; }
    public decimal DefaultPrice { get; set; }
    public ICollection<CoachPackagePrice> PackagePricesList { get; set; } = [];
    public ICollection<CustomerPackage> Customers { get; set; }
}

public class PackageEntityTypeConfiguration : AuditableEntityTypeConfiguration<Package>
{
    public override void Configure(EntityTypeBuilder<Package> builder)
    {
        builder.HasMany(e => e.PackagePricesList)
        .WithOne(e => e.Package)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Customers)
        .WithOne(cp => cp.Package)
        .OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
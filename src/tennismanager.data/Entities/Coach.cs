using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class Coach : User
{
    public ICollection<CoachPackagePrice> PackagePricesList { get; set; } = [];
}

public class CoachEntityTypeConfiguration : UserEntityTypeConfiguration<Coach>
{
    public override void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.HasMany(e => e.PackagePricesList)
        .WithOne(e => e.Coach)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
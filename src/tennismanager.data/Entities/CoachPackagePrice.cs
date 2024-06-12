using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager_api.tennismanager.data.Entities.Abstract;

namespace tennismanager_api.tennismanager.data.Entities;

public class CoachPackagePrice : AuditableEntity
{
    public Guid CoachId { get; set; }
    public Coach Coach { get; set; }
    public Guid PackageId { get; set; }
    public Package Package { get; set; }
    public decimal Price { get; set; }
}

public class CoachPackagePriceEntityTypeConfiguration : AuditableEntityTypeConfiguration<CoachPackagePrice>
{
    public override void Configure(EntityTypeBuilder<CoachPackagePrice> builder)
    {
        builder.HasOne(e => e.Coach)
            .WithMany(e => e.PackagePricesList)
            .HasForeignKey(cpp => cpp.CoachId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Package)
            .WithMany(e => e.PackagePricesList)
            .HasForeignKey(cpp => cpp.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
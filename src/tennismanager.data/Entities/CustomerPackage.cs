using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class CustomerPackage : AuditableEntity
{
  public Guid CustomerId { get; set; }
  public Customer Customer { get; set; }
  public Guid PackageId { get; set; }
  public Package Package { get; set; }
  public DateTime DatePurchased { get; set; }
  public int UsesRemaining { get; set; }
}

public class CustomerPackageEntityTypeConfiguration : AuditableEntityTypeConfiguration<CustomerPackage>
{
  public override void Configure(EntityTypeBuilder<CustomerPackage> builder)
  {
    // Configure properties
    builder.Property(c => c.DatePurchased).IsRequired();
    builder.Property(c => c.UsesRemaining).IsRequired();

    // Define relationships
    builder.HasOne(cp => cp.Customer)
    .WithMany(c => c.Packages)
    .HasForeignKey(cp => cp.CustomerId)
    .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(cp => cp.Package)
    .WithMany(p => p.Customers)
    .HasForeignKey(cp => cp.PackageId)
    .OnDelete(DeleteBehavior.Restrict);
  }
}
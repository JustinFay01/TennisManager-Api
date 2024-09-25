using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class Package : BaseEntity
{
    public string Name { get; set; }
    public Customer Customer { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Price { get; set; }
    public int Uses { get; set; }
    public Coach Coach { get; set; }
    public Guid CoachId { get; set; }
    public DateTime PurchaseDate { get; set; }
}

public class PackageEntityTypeConfiguration : IEntityTypeConfiguration<Package>
{
    public void Configure(EntityTypeBuilder<Package> builder)
    {
    }
}
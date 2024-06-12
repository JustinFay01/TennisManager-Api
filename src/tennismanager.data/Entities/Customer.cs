using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager_api.tennismanager.data.Entities.Abstract;

namespace tennismanager_api.tennismanager.data.Entities;

public class Customer : User
{
    public ICollection<CustomerSession> ParticipatedSessions { get; set; } = [];
    public ICollection<CustomerPackage> Packages { get; set; } = [];
}

public class CustomerEntityTypeConfiguration : UserEntityTypeConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Configure the relationships with other entities
        builder.HasMany(e => e.ParticipatedSessions)
        .WithOne(e => e.Customer)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Packages)
        .WithOne(e => e.Customer)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace tennismanager_api.tennismanager.data.Entities.Abstract;

public abstract class User : BaseEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}

public abstract class UserEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : User
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(c => c.FirstName).IsRequired();
        builder.Property(c => c.LastName).IsRequired();
        builder.Property(c => c.Email);
        builder.Property(c => c.PhoneNumber);
    }
}
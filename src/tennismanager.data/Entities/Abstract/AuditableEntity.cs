using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace tennismanager.data.Entities.Abstract;

public abstract class AuditableEntity : BaseEntity<Guid>, IAuditable
{
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public Guid? UpdatedById { get; set; }
    public User? UpdatedBy { get; set; }
}

public abstract class AuditableEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnType("uuid")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        // AuditableEntity Properties
        builder.Property(e => e.CreatedOn).IsRequired();
        builder.Property(e => e.UpdatedOn);
        builder.Property(e => e.CreatedById).IsRequired();
        builder.Property(e => e.UpdatedById);

        builder.HasOne(e => e.CreatedBy)
            .WithMany()
            .HasForeignKey(e => e.CreatedById)
            .IsRequired();

        builder.HasOne(e => e.UpdatedBy)
            .WithMany()
            .HasForeignKey(e => e.UpdatedById);
    }
}
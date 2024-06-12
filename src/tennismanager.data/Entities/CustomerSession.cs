using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager_api.tennismanager.data.Entities.Abstract;

namespace tennismanager_api.tennismanager.data.Entities;

public class CustomerSession : AuditableEntity
{
    public decimal? Price { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid SessionId { get; set; }
    public Session Session { get; set; }
}

public class CustomerSessionEntityTypeConfiguration : AuditableEntityTypeConfiguration<CustomerSession>
{
    public override void Configure(EntityTypeBuilder<CustomerSession> builder)
    {        
        builder.HasOne(cs => cs.Customer)
        .WithMany(c => c.ParticipatedSessions)
        .HasForeignKey(cs => cs.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cs => cs.Session)
        .WithMany(s => s.CustomerSessions)
        .HasForeignKey(cs => cs.SessionId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
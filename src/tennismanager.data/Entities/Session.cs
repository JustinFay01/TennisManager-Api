using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager_api.tennismanager.data.Entities.Abstract;

namespace tennismanager_api.tennismanager.data.Entities;

public enum SessionType
{
    Event,
    TennisPrivate,
    TennisDrill,
    TennisHitting,
    PicklePrivate,
    PickleDrill,
    PickleHitting
}

public class Session : AuditableEntity
{
    public SessionType Type { get; set; }
    public DateTime Date { get; set; }
    public ICollection<CustomerSession> CustomerSessions { get; set; } = [];
}

public class SessionEntityConfiguration : AuditableEntityTypeConfiguration<Session>
{
    public override void Configure(EntityTypeBuilder<Session> builder)
    {
        // Session Properties
        builder.Property(e => e.Type).IsRequired()
        .HasConversion<string>();
        
        builder.HasMany(s => s.CustomerSessions)
        .WithOne(cs => cs.Session)
        .HasForeignKey(cs => cs.SessionId)
        .OnDelete(DeleteBehavior.Cascade);  
    }
}
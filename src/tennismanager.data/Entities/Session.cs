using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

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
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public ICollection<CustomerSession> CustomerSessions { get; set; } = [];
}

public class SessionEntityConfiguration : AuditableEntityTypeConfiguration<Session>
{
    public override void Configure(EntityTypeBuilder<Session> builder)
    {
        // Session Properties
        builder.HasDiscriminator<string>("Type")
            .HasValue<Session>(SessionType.Event.ToString())
            .HasValue<PrivateSession>(SessionType.TennisPrivate.ToString())
            .HasValue<Session>(SessionType.TennisDrill.ToString())
            .HasValue<Session>(SessionType.TennisHitting.ToString())
            .HasValue<PrivateSession>(SessionType.PicklePrivate.ToString())
            .HasValue<Session>(SessionType.PickleDrill.ToString())
            .HasValue<Session>(SessionType.PickleHitting.ToString());
        
        builder.HasMany(s => s.CustomerSessions)
        .WithOne(cs => cs.Session)
        .HasForeignKey(cs => cs.SessionId)
        .OnDelete(DeleteBehavior.Cascade);
        
    }
}
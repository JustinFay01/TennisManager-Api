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
    public SessionType Type { get; set; }
    public ICollection<CustomerSession> CustomerSessions { get; set; } = [];
    public Coach? Coach { get; set; }
    public Guid? CoachId { get; set; }
}

public class SessionEntityConfiguration : AuditableEntityTypeConfiguration<Session>
{
    public override void Configure(EntityTypeBuilder<Session> builder)
    {
        // Session Properties
        builder.Property(s => s.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.Date)
            .IsRequired();
        
        builder.HasMany(s => s.CustomerSessions)
        .WithOne(cs => cs.Session)
        .HasForeignKey(cs => cs.SessionId)
        .OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
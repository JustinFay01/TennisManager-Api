using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;
using tennismanager.data.Entities.Events;
using tennismanager.shared.Types;

namespace tennismanager.data.Entities;

public class Session : BaseEntity
{
    public required string Name { get; set; }

    /// <summary>
    ///     Different types of sessions. This could include events, tennis private, tennis drill, tennis hitting, pickleball
    ///     private, pickleball drill, pickleball hitting
    /// </summary>
    public SessionType Type { get; set; }
    
    public required Event Event { get; set; }
    public Guid EventId { get; set; }

    public int Capacity { get; set; }

    /// <summary>
    ///     Coach for private sessions
    /// </summary>
    public Coach? Coach { get; set; }

    public Guid? CoachId { get; set; }
    public string? Description { get; set; }
    
    /// <summary>
    ///     Customers who have signed up for this session
    /// </summary>
    public ICollection<CustomerSession> CustomerSessions { get; set; } = [];
}

public class SessionEntityConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.Property(s => s.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(500);
    }
}
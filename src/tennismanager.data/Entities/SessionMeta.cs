using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class SessionMeta : BaseEntity
{
    public Session Session { get; set; }
    public Guid SessionId { get; set; }

    /// <summary>
    ///     Whether the session is recurring or not.
    /// </summary>
    public bool Recurring { get; set; }

    /// <summary>
    ///     The date and time of the first iteration of ANY recurring event.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     The date that the recurring event ends. If null, the event is infinite.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    ///     The information about a single recurring event. See SessionInterval for more information.
    /// </summary>
    public List<SessionInterval> SessionIntervals { get; set; } = [];

    public int CurrentWeekDay => (int)StartDate.DayOfWeek;
    public int CurrentWeekOfMonth => StartDate.Day / 7;
}

public class SessionMetaEntityConfiguration : IEntityTypeConfiguration<SessionMeta>
{
    public void Configure(EntityTypeBuilder<SessionMeta> builder)
    {
        builder.Property(sm => sm.Recurring)
            .IsRequired();

        builder.Property(sm => sm.StartDate)
            .IsRequired();

        builder.HasOne(sm => sm.Session)
            .WithOne(s => s.SessionMeta)
            .HasForeignKey<SessionMeta>(sm => sm.SessionId);
    }
}
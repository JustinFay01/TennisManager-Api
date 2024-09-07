using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class SessionMeta : BaseEntity
{
    public Session Session { get; set; }
    public Guid SessionId { get; set; }
    public bool Recurring { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
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
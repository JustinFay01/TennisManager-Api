using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace tennismanager.data.Entities;

public class PrivateSession : Session
{
    public Coach Coach { get; set; }
    public Guid CoachId { get; set; }
}

public class PrivateSessionEntityTypeConfiguration : IEntityTypeConfiguration<PrivateSession>
{
    public void Configure(EntityTypeBuilder<PrivateSession> builder)
    {
        builder.HasOne(ps => ps.Coach)
            .WithMany()
            .HasForeignKey(ps => ps.CoachId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.CustomerSessions)
            .WithOne(cs => cs.Session as PrivateSession)
            .HasForeignKey(cs => cs.SessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
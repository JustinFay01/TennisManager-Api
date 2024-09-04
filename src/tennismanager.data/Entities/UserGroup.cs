using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public enum UserGroupType
{
    AccountOwner,
    Spouse,
    Child,
    Other
}

public class UserGroup
{
    public Group Group { get; set; }
    public int MemberNumber { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }

    public UserGroupType Type { get; set; }
}

public class UserGroupTypeConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasKey(x => new { x.MemberNumber, x.UserId });

        builder.Property(ug => ug.Type)
            .IsRequired()
            .HasConversion<string>();
    }
}
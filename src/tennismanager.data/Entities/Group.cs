using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace tennismanager.data.Entities;

public class Group
{
    public int MemberNumber { get; set; }
}

public class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(x => x.MemberNumber);

        builder.Property(x => x.MemberNumber)
            .HasMaxLength(5);
    }
}
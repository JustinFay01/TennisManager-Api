using Microsoft.EntityFrameworkCore;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;

namespace tennismanager.data;

public class TennisManagerContext : DbContext
{
    public TennisManagerContext(DbContextOptions<TennisManagerContext> options) : base(options)
    {
    }

    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }

    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Package> Packages { get; set; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerSession> CustomerSessions { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionMeta> SessionMetas { get; set; }
    public DbSet<SessionInterval> SessionIntervals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SessionEntityConfiguration).Assembly);

        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Customer>("Customer")
            .HasValue<Coach>("Coach")
            .HasValue<Admin>("Admin");

        base.OnModelCreating(modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;
using tennismanager.shared;
using tennismanager.shared.Utilities;

namespace tennismanager.data;

public class TennisManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<CustomerSession> CustomerSessions { get; set; }
    public DbSet<CustomerPackage> CustomerPackages { get; set; }
    public DbSet<CoachPackagePrice> CoachPackagePrices { get; set; }
    
    public TennisManagerContext(DbContextOptions<TennisManagerContext> options) : base(options)
    {
    }
    
    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Only need to specify one of these even if they extend different configuration types 
        // (AuditableEntity... && UserEntity...) because they both extend IEntityTypeConfiguration
        // and are within the same assembly 
        //
        // Also, do not call base because this will apply those changes automatically
        // if base is called then it will call the configuration twice
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoachEntityTypeConfiguration).Assembly);
        
        // User, Customer, and Coach hierarchy
        modelBuilder.Entity<User>()
        .HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Customer>("Customer")
            .HasValue<Coach>("Coach");
    }

    public override int SaveChanges()
    {
        AuditChanges(SystemUserIds.JustinFayId);
        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        AuditChanges(SystemUserIds.JustinFayId);
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected virtual void AuditChanges(string user)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(x => x.Entity is AuditableEntity && 
                        (x.State == EntityState.Added 
                         || x.State == EntityState.Modified));
        
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((AuditableEntity)entry.Entity).CreatedById = new Guid(user);
                ((AuditableEntity)entry.Entity).CreatedOn = DateTimeFactory.UtcNow; // For testing purposes
                continue; // we don't need when it was updated because it was just created
            }
            // else it is modified 
            ((AuditableEntity)entry.Entity).UpdatedById = new Guid(user);
            ((AuditableEntity)entry.Entity).UpdatedOn = DateTimeFactory.UtcNow;
        }
    }

    #endregion
}
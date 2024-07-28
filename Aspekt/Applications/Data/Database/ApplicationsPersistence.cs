namespace Aspekt.Applications.Data.Database;

using Microsoft.EntityFrameworkCore;

public sealed class ApplicationsPersistence(DbContextOptions<ApplicationsPersistence> options) : DbContext(options)
{
    private const string Schema = "Applications";

    public DbSet<Application> Applications => Set<Application>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new ApplicationEntityConfiguration());
    }
}

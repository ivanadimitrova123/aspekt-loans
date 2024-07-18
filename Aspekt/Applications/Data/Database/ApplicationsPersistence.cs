namespace EvolutionaryArchitecture.Aspekt.Offers.Data.Database;

using Microsoft.EntityFrameworkCore;

internal sealed class OffersPersistence(DbContextOptions<OffersPersistence> options) : DbContext(options)
{
    private const string Schema = "Applications";

    public DbSet<Application> Offers => Set<Application>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());
    }
}

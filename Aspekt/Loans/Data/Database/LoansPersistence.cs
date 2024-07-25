namespace Aspekt.Loans.Data.Database;

using Microsoft.EntityFrameworkCore;

internal sealed class LoansPersistence(DbContextOptions<LoansPersistence> options) : DbContext(options)
{
    private const string Schema = "Loans";

    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new LoanEntityConfiguration());
    }
}
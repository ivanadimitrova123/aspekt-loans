namespace Aspekt.Contacts.Data.Database;

using Microsoft.EntityFrameworkCore;

public sealed class ContactsPersistence(DbContextOptions<ContactsPersistence> options) : DbContext(options)
{
    private const string Schema = "Contacts";

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new ContactEntityConfiguration());
    }
}

namespace EvolutionaryArchitecture.Aspekt.Offers.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class OfferEntityConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Application");
        builder.HasKey(offers => offers.Id);
        builder.Property(offers => offers.PreparedAt).IsRequired();
        builder.Property(offers => offers.OfferedFromTo).IsRequired();
        builder.Property(offers => offers.OfferedFromDate).IsRequired();
        builder.Property(offers => offers.Discount).IsRequired();
        builder.Property(offers => offers.CustomerId).IsRequired();
    }
}
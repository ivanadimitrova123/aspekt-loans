namespace Aspekt.Applications.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class ApplicationEntityConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Applications");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.ContactId).IsRequired();
        builder.Property(a => a.PreparedAt).IsRequired();
        builder.Property(a => a.ApprovedAt).IsRequired(false);
    }
}
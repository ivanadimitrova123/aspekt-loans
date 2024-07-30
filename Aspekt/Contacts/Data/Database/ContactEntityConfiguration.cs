namespace Aspekt.Contacts.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class ContactEntityConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(contact => contact.Id);
        builder.Property(contact => contact.Name).IsRequired();
        builder.Property(contact => contact.Surname).IsRequired();
        builder.Property(contact => contact.Age).IsRequired();
        builder.Property(contact => contact.PhoneNumber).IsRequired();
        builder.Property(contact => contact.SocialSecurityNumber).IsRequired();
        builder.Property(contact => contact.BankAccountNumber).IsRequired();

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnilShop.Users.Data;

internal class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
{
    public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
    {
        builder.ToTable(nameof(UserStreetAddress));
        builder.Property(item => item.Id)
            .ValueGeneratedNever();

        builder.ComplexProperty(usa => usa.StreetAddress);
    }
}
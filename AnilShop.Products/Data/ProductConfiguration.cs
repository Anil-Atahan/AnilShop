using AnilShop.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnilShop.Products.Data;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    // TODO: Remove them later
    internal static readonly Guid Product1Guid = new Guid("A89F6CD7-4693-457B-9009-02205DBBFE45");
    internal static readonly Guid Product2Guid = new Guid("E4FA19BF-6981-4E50-A542-7C9B26E9EC31");
    internal static readonly Guid Product3Guid = new Guid("17C61E41-3953-42CD-8F88-D3F698869B35");

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Title)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.HasData(GetSampleProducts());
    }

    private IEnumerable<Product> GetSampleProducts()
    {
        yield return new Product(Product1Guid, "title 1", "description", 10.99m);
        yield return new Product(Product2Guid, "title 2", "description", 11.99m);
        yield return new Product(Product3Guid, "title 3", "description", 12.99m);
    }
}
using Architecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Database
{
    public sealed class ProductAuditTrailConfiguration : IEntityTypeConfiguration<ProductAuditTrail>
    {
        public void Configure(EntityTypeBuilder<ProductAuditTrail> builder)
        {
            builder.ToTable(nameof(ProductAuditTrail), nameof(ProductAuditTrail));

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(product => product.Name).IsRequired();

            builder.Property(product => product.Description).IsRequired();

            builder.Property(product => product.Action).IsRequired();

            builder.Property(product => product.Price).IsRequired();

            builder.Property(product => product.DateAdded).IsRequired();

            builder.Property(product => product.Row).IsRequired();

            builder.HasOne(product => product.Product).WithMany(x=>x.ProductAuditTrails).HasForeignKey(x=>x.ProductId);
            builder.HasIndex("ProductId");
        }
    }
}

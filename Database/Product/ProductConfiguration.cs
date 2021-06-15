using Architecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Database
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product), nameof(Product));

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(product => product.Name).IsRequired();

            builder.Property(product => product.Description).IsRequired();

            builder.Property(product => product.DateAdded).IsRequired();

            builder.Property(product => product.DateModified).IsRequired();

            builder.HasOne(product => product.User).WithMany(x=>x.Products).HasForeignKey(x=>x.UserId);
            builder.HasMany(product => product.ProductAuditTrails).WithOne(x => x.Product).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex("UserId");
        }
    }
}

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

            //builder.Property(product => product.User).IsRequired();

            builder.Property(product => product.DateAdded).IsRequired();

            builder.Property(product => product.DateModified).IsRequired();

            //builder.OwnsOne(user => user.Name, userName =>
            //{
            //    userName.Property(name => name.FirstName).HasColumnName(nameof(Name.FirstName)).HasMaxLength(100).IsRequired();

            //    userName.Property(name => name.LastName).HasColumnName(nameof(Name.LastName)).HasMaxLength(200).IsRequired();
            //});

            //builder.OwnsOne(user => user.Email, userEmail =>
            //{
            //    userEmail.Property(email => email.Value).HasColumnName(nameof(Email)).HasMaxLength(300).IsRequired();

            //    userEmail.HasIndex(email => email.Value).IsUnique();
            //});

            builder.HasOne(product => product.User).WithMany(x=>x.Products).HasForeignKey(x=>x.UserId);
            builder.HasIndex("UserId");
        }
    }
}

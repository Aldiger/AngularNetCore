using Architecture.Domain;
using Architecture.Model;

namespace Architecture.Application
{
    public sealed class ProductFactory : IProductFactory
    {
        public Product Create(ProductModel model, User user)
        {
            return new Product
            (
                model.Name,
                model.Description,
                model.Price,
                user.Id
            );
        }
    }
}

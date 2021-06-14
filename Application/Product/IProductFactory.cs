using Architecture.Domain;
using Architecture.Model;

namespace Architecture.Application
{
    public interface IProductFactory
    {
        Product Create(ProductModel model, User user);
    }
}

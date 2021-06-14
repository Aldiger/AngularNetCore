using Architecture.Domain;
using Architecture.Model;
using DotNetCore.Objects;
using DotNetCore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Architecture.Database
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<ProductModel> GetModelAsync(long id);

        Task<Grid<ProductModel>> GridAsync(GridParameters parameters);

        Task<IEnumerable<ProductModel>> ListModelAsync();

        Task UpdateProductAsync(Product product);
    }
}

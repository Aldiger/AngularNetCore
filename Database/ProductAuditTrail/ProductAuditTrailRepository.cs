using Architecture.Domain;
using Architecture.Model;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Architecture.Database
{
    public sealed class ProductAuditTrailRepository : EFRepository<ProductAuditTrail>, IProductAuditTrailRepository
    {
        public ProductAuditTrailRepository(Context context) : base(context) { }

        //public Task<ProductModel> GetModelAsync(long id)
        //{
        //    return Queryable.Where(ProductExpression.Id(id)).Select(ProductExpression.Model).SingleOrDefaultAsync();
        //}

        //public Task<Grid<ProductModel>> GridAsync(GridParameters parameters)
        //{
        //    return Queryable.Select(ProductExpression.Model).GridAsync(parameters);
        //}

        //public async Task<IEnumerable<ProductModel>> ListModelAsync()
        //{
        //    return await Queryable.Select(ProductExpression.Model).ToListAsync();
        //}

        //public Task UpdateProductAsync(Product product)
        //{
        //    return UpdatePartialAsync(new { product.Id, product.Description, product.Name, product.Price });
        //}
    }
}

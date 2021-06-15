using System.Collections;
using System.Collections.Generic;
using Architecture.Domain;
using Architecture.Model;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Database
{
    public sealed class ProductAuditTrailRepository : EFRepository<ProductAuditTrail>, IProductAuditTrailRepository
    {
        public ProductAuditTrailRepository(Context context) : base(context) { }

        public Task<Grid<ProductAuditTrailModel>> GridAsync(GridParameters parameters)
        {
            return Queryable.Select(ProductAuditTrailExpression.Model).GridAsync(parameters);
        }

        public async Task<IList<ProductAuditTrailModel>> GetListAsync(IList<long> productIds)
        {
            return await Queryable.Where(x=>productIds.Contains(x.ProductId)).Select(ProductAuditTrailExpression.Model).ToListAsync();
        }
    }
}

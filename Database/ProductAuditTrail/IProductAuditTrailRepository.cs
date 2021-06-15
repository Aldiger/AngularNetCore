using Architecture.Domain;
using Architecture.Model;
using DotNetCore.Objects;
using DotNetCore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Architecture.Database
{
    public interface IProductAuditTrailRepository : IRepository<ProductAuditTrail>
    {
        Task<IList<ProductAuditTrailModel>> GetListAsync(IList<long> productIds);
        Task<Grid<ProductAuditTrailModel>> GridAsync(GridParameters parameters);
    }
}

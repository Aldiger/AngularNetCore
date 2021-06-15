using Architecture.Model;
using DotNetCore.Objects;
using DotNetCore.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Architecture.Application
{
    public interface IProductAuditTrailService
    {
        Task<List<ProductAuditActionModel>> GetAsync(long productId);
    }
}

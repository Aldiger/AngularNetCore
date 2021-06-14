using Architecture.Domain;
using Architecture.Model;

namespace Architecture.Application
{
    public interface IProductAuditTrailFactory
    {
        ProductAuditTrail Create(ProductAuditTrailModel model, User user);
    }
}

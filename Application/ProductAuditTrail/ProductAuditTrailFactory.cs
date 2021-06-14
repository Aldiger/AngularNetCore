using Architecture.Domain;
using Architecture.Model;

namespace Architecture.Application
{
    public sealed class ProductAuditTrailFactory : IProductAuditTrailFactory
    {
        public ProductAuditTrail Create(ProductAuditTrailModel model, User user)
        {
            return new ProductAuditTrail
            (
                model.Name,model.Description, model.Price,model.ProductId, model.UserId,(AuditRow)model.Row, (AuditAction)model.Action

            );
        }
    }
}

using System;
using Architecture.Domain;
using Architecture.Model;

namespace Architecture.Application
{
    public sealed class ProductAuditTrailFactory : IProductAuditTrailFactory
    {
        public ProductAuditTrail Create(Product product, AuditRow row, AuditAction action, DateTime date)
        {
            return new ProductAuditTrail
            (
                product.Name, product.Description, product.Price, product.Id, product.UserId,row, action, date

            );
        }
    }
}

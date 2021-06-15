using Architecture.Domain;
using Architecture.Model;
using System;
using System.Linq.Expressions;

namespace Architecture.Database
{
    public static class ProductAuditTrailExpression
    {

        public static Expression<Func<ProductAuditTrail, ProductAuditTrailModel>> Model => productAuditTrailModel => new ProductAuditTrailModel
        {
            Id = productAuditTrailModel.Id,
            Name = productAuditTrailModel.Name,
            Description = productAuditTrailModel.Description,
            Price = productAuditTrailModel.Price,
            DateAdded = productAuditTrailModel.DateAdded,
            ProductId = productAuditTrailModel.ProductId,
            Row = (int)productAuditTrailModel.Row,
            Action = (int)productAuditTrailModel.Action,
            ActionName = productAuditTrailModel.Action.ToString(),
            UserId = productAuditTrailModel.UserId
        };

        public static Expression<Func<ProductAuditTrail, bool>> ProductId(long productId)
        {
            return productAuditTrail => productAuditTrail.ProductId == productId;
        }
        public static Expression<Func<Product, bool>> UserId(long userId)
        {
            return product => product.User.Id == userId;
        }

    }
}

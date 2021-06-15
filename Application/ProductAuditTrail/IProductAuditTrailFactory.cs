using System;
using Architecture.Domain;
using Architecture.Model;

namespace Architecture.Application
{
    public interface IProductAuditTrailFactory
    {
        ProductAuditTrail Create(Product product, AuditRow row, AuditAction action, DateTime date);
    }
}

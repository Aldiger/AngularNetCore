using System;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetCore.Domain;

namespace Architecture.Domain
{
    public class ProductAuditTrail : Entity<long>
    {
        public ProductAuditTrail
        (
            string name,
            string description,
            decimal price,
            long productId,
            long userId,
            AuditRow row,
            AuditAction action,
            DateTime date
        )
        {
            Name = name;
            Description = description;
            Price = price;
            UserId = userId;
            ProductId = productId;
            Action = action;
            Row = row;
            DateAdded = date;
        }

        public ProductAuditTrail() { }

        public AuditRow Row { get; set; }
        public AuditAction Action { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public DateTime DateAdded { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }


        public User User { get; set; }
        public Product Product { get; set; }

    }
}

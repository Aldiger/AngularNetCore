using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetCore.Domain;

namespace Architecture.Domain
{
    public class Product : Entity<long>
    {
        public Product
        (
            string name,
            string description,
            decimal price,
            long userId
        )
        {
            Name = name;
            Description = description;
            Price = price;
            UserId = userId;
            SetDateInitial();
        }

        private void SetDateInitial()
        {
            var now = DateTime.UtcNow;
            DateAdded = now;
            DateModified = now;
        }

        public Product(long id) => Id = id;

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }


        [ForeignKey("UserId")]
        public long UserId { get; set; }

        public User User { get; set; }
        public ICollection<ProductAuditTrail> ProductAuditTrails { get; set; }

        public void Update(string name, string description, decimal price)
        {
            Description = description;
            Name = name;
            Price = price;
            DateModified = DateTime.Now;
        }
    }
}

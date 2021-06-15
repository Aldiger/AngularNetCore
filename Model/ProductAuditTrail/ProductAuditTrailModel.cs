using System;

namespace Architecture.Model
{
    public sealed record ProductAuditTrailModel
    {
        public long Id { get; set; }
        public int Row { get; set; }
        public string ActionName { get; set; }
        public int Action { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public DateTime DateAdded { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
    }
}

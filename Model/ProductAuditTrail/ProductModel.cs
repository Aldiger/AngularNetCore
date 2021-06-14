using System;

namespace Architecture.Model
{
    public sealed record ProductAuditTrailModel1
    {
        public int Action { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
    }

    public sealed record ProductAuditTrailModel
    {
        public int Row { get; set; }
        public int Action { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public DateTime DateAdded { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
    }
}

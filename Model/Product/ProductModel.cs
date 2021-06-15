using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Architecture.Model
{
    public sealed record ProductModel
    {
        public long Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public long UserId { get; init; }
    }
}

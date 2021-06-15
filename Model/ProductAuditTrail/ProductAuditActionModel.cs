using System;
using System.Collections.Generic;

namespace Architecture.Model
{
    public sealed record ProductAuditActionModel
    {
        public int Action { get; set; }
        public string ActionName { get; set; }
        public DateTime DateAdded { get; set; }
        public IList<PropertyAuditTrail> Properties { get; set; }
    }
}
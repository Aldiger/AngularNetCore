namespace Architecture.Model
{
    public sealed record PropertyAuditTrail
    {
        public string Property { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
    }
}
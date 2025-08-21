namespace DragonballAPI.Models
{
    public sealed class PlanetsQuery
    {
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public string? Name { get; set; }
        public bool? IsDestroyed { get; set; }
    }
}
namespace DragonBallZ.Models.DTOs
{
    public sealed class CharacterSummary
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Ki { get; set; }
        public string? MaxKi { get; set; }
        public string? Race { get; set; }
        public string? Gender { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Affiliation { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }

    public sealed class PlanetDetail   
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDestroyed { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public List<CharacterSummary>? Characters { get; set; }
    }

    public sealed class PlanetDetailsVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsDestroyed { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<CharacterSummary> Characters { get; set; } = new();
    }
}

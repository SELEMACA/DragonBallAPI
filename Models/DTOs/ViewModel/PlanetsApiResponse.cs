using DragonballAPI.Models;
using System.Text.Json.Serialization;

public class PlanetsApiResponse
{
    [JsonPropertyName("items")] public List<Planets> Items { get; set; } = new();
    [JsonPropertyName("meta")] public Meta? Meta { get; set; }
    [JsonPropertyName("links")] public Links? Links { get; set; }
}
public class Meta
{
    [JsonPropertyName("totalItems")] public int TotalItems { get; set; }
    [JsonPropertyName("itemCount")] public int ItemCount { get; set; }
    [JsonPropertyName("itemsPerPage")] public int ItemsPerPage { get; set; }
    [JsonPropertyName("totalPages")] public int TotalPages { get; set; }
    [JsonPropertyName("currentPage")] public int CurrentPage { get; set; }
}
public class Links
{
    [JsonPropertyName("first")] public string? First { get; set; }
    [JsonPropertyName("previous")] public string? Previous { get; set; }
    [JsonPropertyName("next")] public string? Next { get; set; }
    [JsonPropertyName("last")] public string? Last { get; set; }
}

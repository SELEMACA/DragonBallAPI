using DragonballAPI.Models;

public class PlanetsListVm
{
    public List<Planets> Items { get; set; } = new();
    public Meta? Meta { get; set; }
    public Links? Links { get; set; }

    public int Page { get; set; }
    public int Limit { get; set; }
    public string? Name { get; set; }
    public bool? IsDestroyed { get; set; }

    // Usa links si existen; si no, usa meta; si tampoco, heurística por tamaño de página
    public bool HasPrev =>
        (!string.IsNullOrWhiteSpace(Links?.Previous)) ||
        (Meta != null && Meta.CurrentPage > 1) ||
        Page > 1;

    public bool HasNext =>
        (!string.IsNullOrWhiteSpace(Links?.Next)) ||
        (Meta != null && Meta.CurrentPage < Meta.TotalPages) ||
        (Items?.Count == Limit);
}

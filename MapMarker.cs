namespace sprotyv_map;

public sealed class MapMarker
{
    public string Title { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Info { get; set; } = string.Empty;
    public MapMarkerPosition Position { get; set; } = new();

    public Radzen.Blazor.RadzenGoogleMapMarker ToRadzenMapMarker()
    {
        return new Radzen.Blazor.RadzenGoogleMapMarker()
        {
            Title = this.Title,
            Label = this.Label,
            Position = new() {
                Lat = Position.Latitude,
                Lng = Position.Longitude
            }
        };
    }
}
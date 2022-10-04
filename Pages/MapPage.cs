using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Radzen.Blazor;
using System.Linq;

namespace sprotyv_map.Pages;

public partial class MapPage : ComponentBase
{

    [Inject]
    ILogger<MapPage> Logger { get; init; }
    [Inject]
    HttpClient Http { get; init; }

    private List<RadzenGoogleMapMarker> markers = new();
    private Dictionary<string, MapMarker> markerModels = new();
    private MapMarker selectedMarker = new() { Title = "Натисніть на маркер, щоб отримати додаткову інформацію про цей пункт ТрО" };

    public List<RadzenGoogleMapMarker> Markers { get => markers; }
    public Dictionary<string, MapMarker> MarkerModels { get => markerModels; }
    public MapMarker SelectedMarker { get => selectedMarker; }

    void OnMarkerClick(RadzenGoogleMapMarker rMarker) => selectedMarker = MarkerModels[rMarker.Title];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        List<MapMarker> jsonMarkers = new();
        try
        {
            jsonMarkers = await Http.GetFromJsonAsync<List<MapMarker>>("/data/sprotyv.json") ?? throw new NullReferenceException();
            markers = jsonMarkers.Select(x =>
            {
                markerModels[x.Title] = x;
                return x.ToRadzenMapMarker();
            }).ToList();
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex.Message);
            Logger.LogError(ex.Source);
        }
    }
}
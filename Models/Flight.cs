
namespace FlightApi.Models;

public class Flight
{
    public long Id {get; set;}
    public string? flight_Id {get; set;}
    public string? departureDestination {get; set;}
    public string? arrivalDestination {get; set;}
    public List<Itinerary>? itineraries {get; set;}
}
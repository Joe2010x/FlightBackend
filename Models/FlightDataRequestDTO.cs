namespace FlightApi.Models;

public class FlightDataRequestDTO 
{
    public string? departureCity {get; set; }
    public string? arrivalCity {get; set;}

    public DateTime departureDate {get; set;}
}
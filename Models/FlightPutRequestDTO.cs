namespace FlightApi.Models;

public class FlightPutRequestDTO 
{
    public string? flight_id {get; set;}
    public DateTime departureDateTime {get; set; }

    public int numSeats {get; set; }
}
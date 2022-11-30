namespace FlightApi.Models;

public class Itinerary
{

    public long Id { get; set; }
    public DateTime departureAt {get; set;} 
    public DateTime arriveAt {get; set;}
    public int avaliableSeats {get; set;}
    public List<PriceTag>? prices {get; set;}
}
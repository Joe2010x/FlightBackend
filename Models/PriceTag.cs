namespace FlightApi.Models;

public class PriceTag
{

    public long Id { get; set; }
    public string? currency {get; set;}
    public int adult {get; set;}
    public int child {get; set;}
}
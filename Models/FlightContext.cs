using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace FlightApi.Models;

public class FlightContext : DbContext
{
    public FlightContext (DbContextOptions<FlightContext> options) : base(options)
    {

    }

    public DbSet<Flight> Flights {get; set;} = null! ;
    public DbSet<Itinerary> Itineraries {get; set;} = null! ;
    public DbSet<PriceTag> PriceTags {get; set;} = null! ;
}
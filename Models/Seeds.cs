using FlightApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlightApi.Models;

public static class Seeds
{
    public static void Initialize (FlightContext context)
    {
        var fileName = "data.json";
        var incoming = new List<Flight>();
        using (StreamReader r = new StreamReader(fileName))
        {
            string json = r.ReadToEnd();
            incoming = JsonSerializer.Deserialize<List<Flight>>(json);
        }
        
            if (context.Flights.Any()) { return; }
            foreach ( var flight in incoming!) 
            {
                
            context.Flights.AddRange( flight
            );
            }
            context.SaveChanges();

        
    }
}
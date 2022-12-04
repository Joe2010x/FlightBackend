using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightApi.Models;

namespace FlightApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightContext _context;

        public FlightController(FlightContext context)
        {
            _context = context;
            
            Seeds.Initialize(context);
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            return await _context.Flights
                .Include(f => f.itineraries!)
                .ThenInclude(i => i.prices)
                .ToListAsync();
        }

        [HttpPut]
        public async Task<IActionResult> PutFlight(FlightPutRequestDTO flightInfo)
        {
            var itinerary = new Itinerary();
            var flights = _context.Flights
                .Include(f=> f.itineraries!)
                .ThenInclude(i => i.prices);

            var flight = flights.First(f => f.flight_id == flightInfo.flight_id);
            flight.itineraries!.ForEach(i => {
                var compareResult = DateTime.Compare(i.departureAt, flightInfo.departureDateTime);

                if (compareResult == 0)
                    {
                        i.avaliableSeats -= flightInfo.numSeats;
                        itinerary = i;
                        Console.WriteLine("itinerary found");
                        }
                });
        
             _context.Entry(itinerary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                    throw;   
            }

            return NoContent();
        }

        //Flight query with date
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Flight>>> FindFlight(FlightDataRequestDTO flightData) {

            var result = _context.Flights
                .Include(f=> f.itineraries!)
                .ThenInclude(i => i.prices)
                .Select( f => new Flight() {
                    Id = f.Id,
                    flight_id = f.flight_id,
                    departureDestination = f.departureDestination,
                    arrivalDestination = f.arrivalDestination,
                    itineraries = f.itineraries!
                        .Where(i => 
                            i.departureAt.Date == flightData.departureDate.Date)
                        .ToList()
                })
                .Where(f => 
                    f.departureDestination == flightData.departureCity &&
                    f.arrivalDestination == flightData.arrivalCity)
                .ToListAsync();
            return await result;
        }

        private bool FlightExists(long id)
        {
            return (_context.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

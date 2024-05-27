using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/trips")]
public class TripsController(ITripService tripService, IClientService clientService) : ControllerBase
{
        [HttpGet]
        public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
                if (page <= 0 || pageSize <= 10)
                { 
                        return BadRequest("Invalid arguments.");
                }

                var trips = await tripService.GetTrips(page, pageSize);
                return Ok(trips);
        }
        
        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientRequest request)
        {
                if (await clientService.DoesClientExist(request.Pesel))
                        return BadRequest($"Client with PESEL {request.Pesel} already exists.");

                if (!await tripService.DoesTripExist(idTrip))
                        return NotFound($"Trip with id {idTrip} does not exist.");
                
                if (!await tripService.TripYetToHappen(idTrip))
                        return BadRequest($"Trip with ID {idTrip} already happened.");
                
                if (await tripService.ClientAlreadyAssigned(idTrip, request.Pesel))
                        return BadRequest($"Client with PESEL {request.Pesel} is already assigned to trip {idTrip}.");
                
                if (await tripService.AssignClientToTrip(idTrip, request.Pesel, request.PaymentDate))
                { 
                        return Ok();
                }

                return BadRequest("Unable to assign client to trip.");
        }
}

/*
        w kontrollerze, i ze to z contextu entities
        
        List<Book> books = await context.Books.
        .Include(x => x.IdAuthorNavigation) -> inner join 
        .ToListAsync(cancellationToken)
*/
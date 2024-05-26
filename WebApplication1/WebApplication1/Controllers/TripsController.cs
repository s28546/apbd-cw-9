using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


//trip service i repository do zmiany, assign client request tez zly bedzie, spojrzec na tego linqa z Gettrips
[ApiController]
[Route("api/trips")]
public class TripsController(ITripService tripService) : ControllerBase
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
                //czy z przyszlosci datetime -> jak nie blad
                //czy wycieczka istnieje -> jak nie blad
                //czy wycieczka sie juz odbyla -> jak tak to blad
                //czy klient o nr pesel istnieje -> jak tak to blad
                /*
                 *      PaymentDate może mieć wartość NULL dla tych
                        klientów, którzy jeszcze nie zapłacili za wycieczkę.
                        Ponadto RegisteredAt w tabeli Client_Trip powinna być
                        zgodna z czasem przyjęcia żądania przez serwer.
                 *
                                         *      {
                        "FirstName": "John",
                        "LastName": "Doe",
                        "Email": "doe@wp.pl",
                        "Telephone": "543-323-542",
                        "Pesel": "91040294554",
                        "IdTrip": 10,
                        "TripName": "Rome",
                        "PaymentDate": "4/20/2021"
}
                 * 
                 * 
                 */
                
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
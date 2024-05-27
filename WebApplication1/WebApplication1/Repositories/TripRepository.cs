using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class TripRepository(Cw9Context context) : ITripRepository
{
    public async Task<PaginatedTripsDTO> GetTrips(int page, int pageSize)
    {
        var total = await context.Trips.CountAsync();
        
        var trips = await context.Trips
            .Include(t => t.CountryTrips)
            .ThenInclude(ct => ct.Country)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDTO(
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                t.CountryTrips.Select(ct => new CountryDTO(ct.Country.Name)).ToList(),
                t.ClientTrips.Select(ct => new ClientDTO(ct.IdClientNavigation.FirstName, ct.IdClientNavigation.LastName)).ToList()
            ))
            .ToListAsync();

        var allPages = (int) Math.Ceiling((double) total / pageSize);

        return new PaginatedTripsDTO(page, pageSize, allPages, trips);
    }
    
    public async Task<Trip?> GetTripById(int idTrip)
    {
        return await context.Trips.FindAsync(idTrip);
    }

    public async Task<bool> IsClientAssignedToTrip(int idTrip, int idClient)
    {
        return await context.ClientTrips.AnyAsync(ct => ct.IdTrip == idTrip && ct.IdClient == idClient);
    }

    public async Task AssignClientToTrip(int idTrip, int idClient, DateTime? paymentDate, DateTime registeredAt)
    {
        var clientTrip = new ClientTrip
        {
            IdTrip = idTrip,
            IdClient = idClient,
            PaymentDate = paymentDate,
            RegisteredAt = registeredAt
        };

        context.ClientTrips.Add(clientTrip);
        await context.SaveChangesAsync();
    }
}

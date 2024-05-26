using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class TripRepository : ITripRepository
{
    private readonly Cw9Context _context;

    public TripRepository(Cw9Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TripDTO>> GetTrips()
    {
        return await _context.Trips
            .Include(t => t.Country_Trips)
            .ThenInclude(ct => ct.Country)
            .Include(t => t.Client_Trips)
            .ThenInclude(ct => ct.Client)
            .OrderByDescending(t => t.DateFrom)
            .Select(t => new TripDTO(
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                t.Country_Trips.Select(ct => new CountryDTO(ct.Country.Name)).ToList(),
                t.Client_Trips.Select(ct => new ClientDTO(ct.Client.FirstName, ct.Client.LastName)).ToList()
            ))
            .ToListAsync();
    }

    public async Task<PaginatedTripsDTO> GetTrips(int page, int pageSize)
    {
        var totalTrips = await _context.Trips.CountAsync();
        var trips = await _context.Trips
            .Include(t => t.Country_Trips)
            .ThenInclude(ct => ct.Country)
            .Include(t => t.Client_Trips)
            .ThenInclude(ct => ct.Client)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDTO(
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                t.Country_Trips.Select(ct => new CountryDTO(ct.Country.Name)).ToList(),
                t.Client_Trips.Select(ct => new ClientDTO(ct.Client.FirstName, ct.Client.LastName)).ToList()
            ))
            .ToListAsync();

        var allPages = (int)Math.Ceiling((double)totalTrips / pageSize);

        return new PaginatedTripsDTO(page, pageSize, allPages, trips);
    }
}

public record TripDTO(
    string Name,
    string Description,
    DateTime DateFrom,
    DateTime DateTo,
    int MaxPeople,
    List<CountryDTO> Countries,
    List<ClientDTO> Clients
);

public record CountryDTO(
    string Name
);

public record ClientDTO(
    string FirstName,
    string LastName
);

public record PaginatedTripsDTO(
    int PageNum,
    int PageSize,
    int AllPages,
    List<TripDTO> Trips
);
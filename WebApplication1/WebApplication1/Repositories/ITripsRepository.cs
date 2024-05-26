using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public interface ITripRepository
{
    public Task<IEnumerable<TripDTO>> GetTrips();
    public Task<PaginatedTripsDTO> GetTrips(int page, int pageSize);
    
    //public Task<IEnumerable<Trip>> GetTrips(pagesize)
}
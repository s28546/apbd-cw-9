using WebApplication1.Models;

public interface ITripService
{
    public Task<IEnumerable<TripDTO>> GetTrips();
    public Task<PaginatedTripsDTO> GetTrips(int page, int pageSize);
    
    public Task<bool> AssignClientToTrip(int idTrip, string pesel, DateTime? paymentDate);
}
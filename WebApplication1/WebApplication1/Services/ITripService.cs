using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ITripService
{
    public Task<PaginatedTripsDTO> GetTrips(int page, int pageSize);
    
    public Task<bool> AssignClientToTrip(int idTrip, string pesel, DateTime? paymentDate);
}
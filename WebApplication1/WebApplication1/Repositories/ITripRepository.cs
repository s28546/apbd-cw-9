using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ITripRepository
{
    public Task<PaginatedTripsDTO> GetTrips(int page, int pageSize);
    
    public Task<Trip?> GetTripById(int idTrip);
    public Task<bool> IsClientAssignedToTrip(int idTrip, int idClient);
    public Task AssignClientToTrip(int idTrip, int idClient, DateTime? paymentDate, DateTime registeredAt);
}
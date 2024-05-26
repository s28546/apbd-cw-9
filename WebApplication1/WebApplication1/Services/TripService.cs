using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class TripService(ITripRepository tripRepository, IClientRepository clientRepository)
        : ITripService
    {
        public async Task<PaginatedTripsDTO> GetTrips(int page, int pageSize)
        {
            return await tripRepository.GetTrips(page, pageSize);
        }

        public async Task<bool> AssignClientToTrip(int idTrip, string pesel, DateTime? paymentDate)
        {
            var existingClient = await clientRepository.GetClientByPesel(pesel);
            if (existingClient == null)
            {
                return false; // Client does not exist
            }

            if (await tripRepository.IsClientAssignedToTrip(idTrip, existingClient.IdClient))
            {
                return false; // Client is already assigned to the trip
            }

            var trip = await tripRepository.GetTripById(idTrip);
            if (trip == null || trip.DateFrom <= DateTime.Now)
            {
                return false; // Trip does not exist or DateFrom is in the past
            }

            await tripRepository.AssignClientToTrip(idTrip, existingClient.IdClient, paymentDate, DateTime.Now);
            return true;
        }
    }
}
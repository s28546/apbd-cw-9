using WebApplication1.Entities;
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

        public async Task<bool> TripYetToHappen(int idTrip)
        {
            var trip = await tripRepository.GetTripById(idTrip);
            return trip.DateFrom > DateTime.Now;
        }

        public async Task<bool> ClientAlreadyAssigned(int idTrip, string pesel)
        {
            var client = await clientRepository.GetClientByPesel(pesel);
            var clientTrips = await clientRepository.GetClientTrips(client);
            return clientTrips.Any(ct => ct.IdTrip == idTrip);
        }

        public async Task<bool> DoesTripExist(int tripId)
        {
            var trip = await tripRepository.GetTripById(tripId);
            return trip != null;
        }

        public async Task<bool> AssignClientToTrip(int idTrip, string pesel, DateTime? paymentDate)
        {
            var client = await clientRepository.GetClientByPesel(pesel);
            var trip = await tripRepository.GetTripById(idTrip);
            
            await tripRepository.AssignClientToTrip(idTrip, client.IdClient, paymentDate, DateTime.Now);
            return true;
        }
    }
}
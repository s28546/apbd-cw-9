using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public interface IClientRepository
{
    Task<bool> DeleteClient(int idClient);
    Task<Client?> GetClientById(int idClient); 
    Task<Client?> GetClientByPesel(string pesel);
    Task<bool> DoesClientHaveTrips(int idClient);
}
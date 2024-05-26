using WebApplication1.Entities;

namespace WebApplication1.Services;

public interface IClientService
{
    Task<bool> DeleteClient(int idClient);
    Task<bool> DoesClientExist(int idClient);
    Task<bool> DoesClientHaveTrips(int idClient);
}
using WebApplication1.Entities;

namespace WebApplication1.Services;

public interface IClientService
{
    Task<Client?> DeleteClient(int idClient);
}
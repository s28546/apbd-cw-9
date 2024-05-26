using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<Client?> DeleteClient(int idClient)
    {
        if (await clientRepository.HasTrips(idClient))
        {
            return null;
        }

        return await clientRepository.DeleteClient(idClient);
    }
}

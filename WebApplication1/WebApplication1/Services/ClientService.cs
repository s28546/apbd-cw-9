using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<bool> DeleteClient(int idClient)
    {
        return await clientRepository.DeleteClient(idClient);
    }

    public async Task<bool> DoesClientHaveTrips(int idClient)
    {
        return await clientRepository.DoesClientHaveTrips(idClient);
    }
    
    public async Task<bool> DoesClientExist(int idClient)
    {
        var client = await clientRepository.GetClientById(idClient);
        return client != null;
    }
    
    public async Task<bool> DoesClientExist(string pesel)
    {
        var client = await clientRepository.GetClientByPesel(pesel);
        return client != null;
    }
}

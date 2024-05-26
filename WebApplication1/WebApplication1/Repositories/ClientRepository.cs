using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class ClientRepository(Cw9Context context) : IClientRepository
{

    public async Task<bool> HasTrips(int idClient)
    {
        return await context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
    }

    public async Task<Client?> DeleteClient(int idClient)
    {
        var client = await context.Clients.FindAsync(idClient);
        if (client != null)
        {
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
        }
        return client;
    }
    
    public async Task<Client?> GetClientByPesel(string pesel)
    {
        return await context.Clients.FirstOrDefaultAsync(c => c.Pesel == pesel);
    }
}
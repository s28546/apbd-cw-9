using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class ClientRepository(Cw9Context context) : IClientRepository
{
    
    public async Task<bool> DeleteClient(int idClient)
    {
        var client = await context.Clients.FindAsync(idClient);
        if (client != null)
        {
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<List<ClientTrip>> GetClientTrips(Client client)
    {
        return await context.ClientTrips
            .Include(ct => ct.IdTripNavigation)
            .Include(ct => ct.IdClientNavigation)
            .Where(ct => ct.IdClient == client.IdClient)
            .ToListAsync();
    }
    
    public async Task<bool> DoesClientHaveTrips(int idClient)
    {
        return await context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);
    }
    
    public async Task<Client?> GetClientById(int idClient)
    {
        return await context.Clients.FindAsync(idClient);
    }
    
    public async Task<Client?> GetClientByPesel(string pesel)
    {
        return await context.Clients.FirstOrDefaultAsync(c => c.Pesel == pesel);
    }
}
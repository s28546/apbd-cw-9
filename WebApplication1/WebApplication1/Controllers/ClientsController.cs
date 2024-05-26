using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        if (!await clientService.DoesClientExist(idClient))
        {
            return NotFound($"Client with ID {idClient} does not exist.");
        }

        if (await clientService.DoesClientHaveTrips(idClient))
        {
            return BadRequest("Client cannot be deleted because they have trips assigned.");
        }

        await clientService.DeleteClient(idClient);

        return Ok($"Client with ID {idClient} deleted.");
    }
}

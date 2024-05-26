using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class Controller : ControllerBase
{
    
}

/*
        w kontrollerze, i ze to z contextu entities
        
        List<Book> books = await context.Books.
        .Include(x => x.IdAuthorNavigation) -> inner join 
        .ToListAsync(cancellationToken)
*/
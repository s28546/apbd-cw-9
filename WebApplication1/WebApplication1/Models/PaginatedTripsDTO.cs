namespace WebApplication1.Models;

public record PaginatedTripsDTO(
    int PageNum,
    int PageSize,
    int AllPages,
    List<TripDTO> Trips
);
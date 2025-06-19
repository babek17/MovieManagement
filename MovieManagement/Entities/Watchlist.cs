using MovieManagement.Entities;

namespace MovieManagement.Entities;

public class Watchlist
{
    public int WatchlistId { get; set; }
    
    public required string UserId { get; set; }
    
    public ApplicationUser User { get; set; }

    public required int MovieId { get; set; }
    public required Movie Movie { get; set; }
}
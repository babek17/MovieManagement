using MovieManagement.Entities;

namespace MovieManagement.Entities;

public class Watchlist
{
    public int WatchlistId { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}
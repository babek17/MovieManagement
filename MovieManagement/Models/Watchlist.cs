using MovieManagement.Entities;

namespace MovieManagement.Models;

public class Watchlist
{
    public int WatchlistId { get; set; }  // PK

    public string UserId { get; set; }    // FK to ApplicationUser.Id
    public ApplicationUser User { get; set; }

    public int MovieId { get; set; }      // FK to Movie.Id
    public Movie Movie { get; set; }
}
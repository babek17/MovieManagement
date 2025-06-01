using Microsoft.AspNetCore.Identity;
using MovieManagement.Models;

namespace MovieManagement.Entities;

public class ApplicationUser: IdentityUser
{
    public string UniqueUsername { get; set; }
    public List<Movie> WatchlistMovies = new List<Movie>();
    public List<Rating> Ratings { get; set; }
    public List<Comment> Comments { get; set; }
    public ICollection<Watchlist> Watchlist { get; set; } = new List<Watchlist>();
    
}
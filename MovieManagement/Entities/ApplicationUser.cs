using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MovieManagement.Models;
using MovieManagement.MongoEntities;

namespace MovieManagement.Entities;

public class ApplicationUser: IdentityUser
{
    public List<Movie> WatchlistMovies = new List<Movie>();
    public List<Rating> Ratings { get; set; } = new List<Rating>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Watchlist> Watchlist { get; set; } = new List<Watchlist>();
    
}
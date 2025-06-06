using System.ComponentModel.DataAnnotations;
using MovieManagement.Models;

namespace MovieManagement.Entities;

public class Movie
{
    public int MovieId { get; set; }
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(50)]
    public string Genre { get; set; }
    public int DirectorId { get; set; }
    public Director? Director { get; set; }
    public decimal Rating { get; set; } = 0;
    public int RatingCount { get; set; } = 0;
    public int RunningTime { get; set; }
    public int ReleaseYear { get; set; }
    public string ImageUrl { get; set; }
    public string TrailerUrl { get; set; }
    public List<Comment> Comments { get; set; }= new List<Comment>();
    public List<ApplicationUser> Users { get; set; }= new List<ApplicationUser>();
    public ICollection<Watchlist> WatchlistUsers { get; set; } = new List<Watchlist>();
    public string ShortDescription { get; set; }
}

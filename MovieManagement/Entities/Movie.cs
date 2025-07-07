using System.ComponentModel.DataAnnotations;
using MovieManagement.Attributes;
using MovieManagement.MongoEntities;

namespace MovieManagement.Entities;

public class Movie
{
    public int MovieId { get; set; }
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(50)]
    public required string Genre { get; set; }
    public int DirectorId { get; set; }
    public required Director Director { get; set; }
    public decimal Rating { get; set; } = 0;
    public int RatingCount { get; set; } = 0;
    
    [Range(1, 50000, ErrorMessage = "Running time must be a positive number.")]
    public required int RunningTime { get; set; }
    
    [ValidReleaseYear]
    public required int ReleaseYear { get; set; }
    
    [MaxLength(500)]
    public required string ImageUrl { get; set; }
    [MaxLength(500)]
    public required string TrailerUrl { get; set; }
    public List<Comment> Comments { get; set; }= new List<Comment>();
    public List<ApplicationUser> Users { get; set; }= new List<ApplicationUser>();
    public ICollection<Watchlist> WatchlistUsers { get; set; } = new List<Watchlist>();
    [MaxLength(2000)]
    public required string ShortDescription { get; set; }
}

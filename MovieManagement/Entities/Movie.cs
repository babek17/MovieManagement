using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Entities;

public class Movie
{
    public int MovieId { get; set; }
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(50)]
    public string Genre { get; set; }
    [MaxLength(100)]
    public required string DirectorName { get; set; }
    public Director? Director { get; set; }
    public int RunningTime { get; set; }
    public int ReleaseYear { get; set; }
    public string ImageUrl { get; set; }
    public string TrailerUrl { get; set; }
    public float Rating { get; set; } = 0;
    public List<Comment> Comments { get; set; }= new List<Comment>();
    public List<ApplicationUser> Users { get; set; }= new List<ApplicationUser>();
}
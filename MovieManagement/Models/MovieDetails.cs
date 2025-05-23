using MovieManagement.Entities;

namespace MovieManagement.Models;

public class MovieDetails
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string Genre { get; set; }
    public required string DirectorName { get; set; }
    public int RunningTime { get; set; }
    public int ReleaseYear { get; set; }
    public string ImageUrl { get; set; }
    public string TrailerUrl { get; set; }
    public float Rating { get; set; } = 0;
    public List<Comment> Comments { get; set; }= new List<Comment>();
    public string Description { get; set; }
}
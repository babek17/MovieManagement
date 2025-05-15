using MovieManagement.Repositories;

namespace MovieManagement.Models;

public class MovieCard
{
    public string Title { get; set; }
    public float Rating { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public string ImageUrl { get; set; }
}
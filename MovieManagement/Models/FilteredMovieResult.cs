using MovieManagement.Entities;

namespace MovieManagement.Models;

public class FilteredMovieResult
{
    public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
}
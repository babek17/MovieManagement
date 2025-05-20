using MovieManagement.Entities;

namespace MovieManagement.Services;

public interface IMovieService
{
    IQueryable<Movie> Sort(string sortBy);
    Task<IEnumerable<Movie>> SearchAsync(string query);
    Movie GetMovieById(int movieId);
    IQueryable<Movie> GetAllMovies();
}
using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public interface IMovieRepository
{
    IQueryable<Movie> GetAllMovies();
    Movie GetMovieById(int id);
    Movie GetMovieByTitle(string title);
    List<Movie> GetAllMoviesByDirector(int id);
    Task<IEnumerable<Movie>> SearchMoviesAsync(string query);
    IQueryable<Movie> SortMoviesAsync(string sortBy);
}
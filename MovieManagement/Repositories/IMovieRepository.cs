using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public interface IMovieRepository
{
    List<Movie> GetAllMovies();
    Movie GetMovieById(int id);
    Movie GetMovieByTitle(string title);
    List<Movie> GetAllMoviesByDirector(int id);
    Task<IEnumerable<Movie>> SearchMoviesAsync(string query);
}
using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public interface IMovieRepository
{
    IQueryable<Movie> GetAllMovies();
    Movie GetMovieById(int id);
    Movie GetMovieByTitle(string title);
    List<Movie> GetAllMoviesByDirector(int id);
    Task<IEnumerable<Movie>> SearchMoviesAsync(string query);
    IQueryable<Movie> SortMoviesAsync(IQueryable<Movie> movies,string sortBy);
    void Save();
    void Add(Movie movie);
    void Remove(int movieId);
}
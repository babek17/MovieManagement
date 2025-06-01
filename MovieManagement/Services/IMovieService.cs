using MovieManagement.Entities;
using MovieManagement.Models;

namespace MovieManagement.Services;

public interface IMovieService
{
    IQueryable<Movie> Sort(IQueryable<Movie> movie,string sortBy);
    Task<IEnumerable<Movie>> SearchAsync(string query);
    Movie GetMovieById(int movieId);
    IQueryable<Movie> GetAllMovies();
    IEnumerable<MovieCard> BuildMovieCards(IEnumerable<Movie> movies, HashSet<int> userWatchlistMovieIds);
    FilteredMovieResult GetFilteredMoviesAsync(MovieQuery query);
}
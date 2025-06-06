using MovieManagement.Entities;
using MovieManagement.Models;

namespace MovieManagement.Services;

public interface IMovieService
{
    IQueryable<Movie> Sort(IQueryable<Movie> movie, string sortBy);
    Task<IEnumerable<Movie>> SearchAsync(string query);
    Movie GetMovieById(int movieId);
    IQueryable<Movie> GetAllMovies();
    IEnumerable<MovieCard> BuildMovieCards(IEnumerable<Movie> movies, HashSet<int> userWatchlistMovieIds);
    FilteredMovieResult GetFilteredMoviesAsync(MovieQuery query);
    void RateMovie(int movieId, string userId, int score);
    void AddComment(int movieId, string userId, string comment);
    IQueryable<Comment> GetCommentsForMovie(int movieId);
    int AddMovieAsync(MovieDetails model, int movieId);
    void RemoveMovie(int movieId);
}
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.MongoEntities;

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
    Task AddCommentAsync(string userId, int movieId, string commentText);
    Task AddCommentAsync(string userId, string userName, int movieId, string commentText, string? parentCommentId);
    Task DeleteCommentAsync(string userId, int movieId);
    Task<List<Comment>> GetCommentsForMovie(int movieId);
    void RemoveMovie(int movieId);
    Task AddMovieAsync(MovieDetails model, string rootPath, int directorId);
    void EditMovieAsync(MovieDetails model);
    Task<List<CommentDto>> GetNestedCommentsAsync(int movieId);
}
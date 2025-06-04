using MovieManagement.Entities;
using MovieManagement.Models;

namespace MovieManagement.Services;

public interface IUserServices
{
    Task<bool> AddMovieAsync(string userId, Movie movie);
    Task<bool> RemoveMovieAsync(string userId, int movieId);
    Task<List<MovieCard>> GetUserMovieCardsAsync(string userId);
    int? GetUserRating(string userId, int movieId);
}
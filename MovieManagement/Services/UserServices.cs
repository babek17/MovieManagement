using MovieManagement.Entities;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Models;

namespace MovieManagement.Services;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> AddMovieAsync(string userId, Movie movie)
    {
        var user = await _userRepository.GetUserWithWatchlistAsync(userId);
        if (user == null) return false;

        // Check if the movie is already in the user's watchlist
        bool alreadyAdded = user.Watchlist.Any(w => w.MovieId == movie.MovieId);
        if (alreadyAdded) return false;

        // Create a new Watchlist entry
        var watchlistItem = new Watchlist
        {
            UserId = userId,
            MovieId = movie.MovieId,
            Movie = movie // optional, can be null if EF tracks movies separately
        };

        user.Watchlist.Add(watchlistItem);

        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveMovieAsync(string userId, int movieId)
    {
        var user = await _userRepository.GetUserWithWatchlistAsync(userId);
        if (user == null) return false;

        var watchlistItem = user.Watchlist.FirstOrDefault(w => w.MovieId == movieId);
        if (watchlistItem == null) return false;

        user.Watchlist.Remove(watchlistItem);

        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<List<MovieCard>> GetUserMovieCardsAsync(string userId)
    {
        var user = await _userRepository.GetUserWithWatchlistAsync(userId);
        if (user == null || user.Watchlist == null)
            return new List<MovieCard>();

        // Map each Movie to a MovieCard
        var movieCards = user.Watchlist
            .Select(w => new MovieCard
            {
                Id = w.Movie.MovieId,
                Title = w.Movie.Title,
                Year = w.Movie.ReleaseYear,
                Genre = w.Movie.Genre,
                ImageUrl = w.Movie.ImageUrl
            }).ToList();

        return movieCards;
    }
}
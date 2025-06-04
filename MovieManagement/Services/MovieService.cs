using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Repositories;

namespace MovieManagement.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IRatingRepository _ratingRepository;

    public MovieService(IMovieRepository movieRepository, IRatingRepository ratingRepository)
    {
        _movieRepository = movieRepository;
        _ratingRepository = ratingRepository;
    }
    
    public async Task<IEnumerable<Movie>> SearchAsync(string query)
    {
        
        return await _movieRepository.SearchMoviesAsync(query);
    }
    
    public IQueryable<Movie> Sort(IQueryable<Movie> movies, string sortBy)
    {
        return  _movieRepository.SortMoviesAsync(movies,sortBy);
    }
    

    public Movie GetMovieById(int movieId)
    {
        return _movieRepository.GetMovieById(movieId);
    }

    public IQueryable<Movie> GetAllMovies()
    {
        return _movieRepository.GetAllMovies();
    }
    
    public IEnumerable<MovieCard> BuildMovieCards(IEnumerable<Movie> movies, HashSet<int> userWatchlistMovieIds)
    {
        return movies.Select(mc => new MovieCard
        {
            Id = mc.MovieId,
            Title = mc.Title,
            Year = mc.ReleaseYear,
            Genre = mc.Genre,
            ImageUrl = mc.ImageUrl,
            IsInWatchlist = userWatchlistMovieIds.Contains(mc.MovieId),
            Rating = mc.Rating
        });
    }

    public FilteredMovieResult GetFilteredMoviesAsync(MovieQuery query)
    {
        var movies = _movieRepository.GetAllMovies();

        if (query.Genre != "All Genres")
            movies = movies.Where(m => m.Genre == query.Genre);

        movies = Sort(movies, query.SortBy);

        int totalCount = movies.Count();
        int totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

        var pagedMovies = movies
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new FilteredMovieResult
        {
            Movies = pagedMovies,
            CurrentPage = query.Page,
            TotalPages = totalPages,
            TotalCount = totalCount
        };
    }
    
    public void RateMovie(int movieId, string userId, int score)
    {
        var existingRating = _ratingRepository.GetRating(userId, movieId);
        if (existingRating != null)
        {
            existingRating.Score = score;
        }
        else
        {
            var newRating = new Rating
            {
                MovieId = movieId,
                UserId = userId,
                Score = score
            };
            _ratingRepository.AddRating(newRating);
        }
    
        _ratingRepository.Save();
    
        var ratingsForMovie =  _ratingRepository.GetAllRatings()
            .Where(r => r.MovieId == movieId)
            .ToList();
    
        var ratingCount = ratingsForMovie.Count;
        var averageRating = ratingCount > 0
            ? (decimal)ratingsForMovie.Sum(r => r.Score) / ratingCount
            : 0m;
    
        var movie = _movieRepository.GetMovieById(movieId);
        movie.RatingCount = ratingCount;
        movie.Rating = Math.Round(averageRating, 1);
        _movieRepository.Save();
    }
    
}
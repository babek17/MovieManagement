using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Repositories;

namespace MovieManagement.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IRatingRepository _ratingRepository;
    private readonly ICommentRepository _commentRepository;

    public MovieService(IMovieRepository movieRepository, IRatingRepository ratingRepository, ICommentRepository commentRepository)
    {
        _movieRepository = movieRepository;
        _ratingRepository = ratingRepository;
        _commentRepository = commentRepository;
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

    public IQueryable<Comment> GetCommentsForMovie(int movieId)
    {
       var comments = _commentRepository.GetCommentsForMovie(movieId);
       return comments;
    }


    public void AddComment(int movieId, string userId, string comment)
    {
        var addedComment = new Comment()
        {
            UserId = userId,
            CommentText = comment,
            MovieId = movieId,
            CommentDate = DateTime.Now
        };
        _commentRepository.Add(addedComment);
        _commentRepository.Save();
    }
    

    public int AddMovieAsync(MovieDetails model, int movieId)
    {
        var movie = new Movie
        {
            Title = model.Title,
            Genre = model.Genre,
            RunningTime = model.RunningTime,
            ReleaseYear = model.ReleaseYear,
            ImageUrl = model.ImageUrl,
            TrailerUrl = model.TrailerUrl,
            ShortDescription = model.Description,
        };
        _movieRepository.Add(movie);
        return movie.MovieId;
    }
    
    public void RemoveMovie(int movieId)
    {
        _movieRepository.Remove(movieId);
    }
}
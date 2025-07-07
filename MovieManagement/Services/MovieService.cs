using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.MongoEntities;
using MovieManagement.Repositories;

namespace MovieManagement.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IRatingRepository _ratingRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IDirectorService _directorService;
    private readonly UserManager<ApplicationUser> _userManager;

    public MovieService(IMovieRepository movieRepository, IRatingRepository ratingRepository, 
        ICommentRepository commentRepository, IDirectorService directorService,
            UserManager<ApplicationUser> userManager)
    {
        _movieRepository = movieRepository;
        _ratingRepository = ratingRepository;
        _commentRepository = commentRepository;
        _directorService = directorService;
        _userManager = userManager;
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
    
    public async Task<List<Comment>> GetCommentsForMovie(int movieId)
    {
       var comments = await _commentRepository.GetCommentsForMovieAsync(movieId);
       return comments;
    }
    public async Task<List<CommentDto>> GetNestedCommentsAsync(int movieId)
    {
        var allComments = await _commentRepository.GetCommentsForMovieAsync(movieId);
        var rootComments = allComments.Where(c => string.IsNullOrEmpty(c.ParentCommentId)).ToList();

        List<CommentDto> BuildHierarchy(List<Comment> comments, string? parentId, int level=0)
        {
            return comments
                .Where(c => c.ParentCommentId == parentId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    MovieId = c.MovieId,
                    UserId = c.UserId,
                    UserName = c.UserName,
                    CommentText = c.CommentText,
                    CommentDate = c.CommentDate,
                    NestingStage = level,
                    Replies = BuildHierarchy(comments, c.Id, level + 1)
                })
                .ToList();
        }

        return BuildHierarchy(allComments, null);
    }

    public async Task AddCommentAsync(string userId, string userName, int movieId, string commentText, string? parentCommentId)
    {
        var comment = new Comment
        {
            UserId = userId,
            UserName = userName,
            MovieId = movieId,
            CommentText = commentText,
            CommentDate = DateTime.UtcNow,
            ParentCommentId = parentCommentId
        };

        await _commentRepository.AddAsync(comment);
    }

    
    public async Task AddCommentAsync(string userId, int movieId, string commentText)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        var comment = new Comment
        {
            MovieId = movieId,
            UserId = userId,
            UserName = user.UserName,
            CommentText = commentText,
            CommentDate = DateTime.UtcNow
        };

        await _commentRepository.AddAsync(comment);
    }
    
    
    
    public async Task DeleteCommentAsync(string userId, int movieId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found");
        
        var comment = await _commentRepository.FindCommentByUserAndMovieIdAsync(userId, movieId);

        await _commentRepository.DeleteAsync(comment);
    }
    
    public void RemoveMovie(int movieId)
    {
        _movieRepository.Remove(movieId);
    }

    public async Task AddMovieAsync(MovieDetails model, string rootPath, int directorId)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        string imagePath = null;
        
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(rootPath, "wwwroot", "Images", "Movies");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = model.Title.ToLower().Trim().Replace(" ", "_") + Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            imagePath = $"/Images/Movies/{uniqueFileName}";
        }
        var movie = new Movie()
        {
            Title = model.Title,
            Genre = model.Genre,
            ImageUrl = imagePath,
            DirectorId = directorId,
            Director = _directorService.GetDirectorById(directorId),
            ReleaseYear = model.ReleaseYear,
            RunningTime = model.RunningTime,
            ShortDescription = model.Description,
            TrailerUrl = model.TrailerUrl
        };
        _movieRepository.Add(movie);
    }
    
    public void EditMovieAsync(MovieDetails model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        var existingMovie = _movieRepository.GetMovieById(model.Id);
        if (existingMovie == null)
            throw new InvalidOperationException($"Movie with ID {model.Id} not found.");
        

        existingMovie.Title = model.Title;
        existingMovie.Genre = model.Genre;
        existingMovie.ReleaseYear = model.ReleaseYear;
        existingMovie.RunningTime = model.RunningTime;
        existingMovie.ShortDescription = model.Description;
        existingMovie.TrailerUrl = model.TrailerUrl;

        existingMovie.DirectorId = model.DirectorId > 0 
            ? model.DirectorId 
            : existingMovie.DirectorId;
        existingMovie.ImageUrl = string.IsNullOrEmpty(model.ImageUrl) 
            ? model.CurrentImageUrl
            : model.ImageUrl;
        
        _movieRepository.Update(existingMovie);
    }

}
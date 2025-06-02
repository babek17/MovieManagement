using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public class MovieRepository: IMovieRepository
{
    private readonly MovieManagementDbContext _context;

    public MovieRepository(MovieManagementDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<Movie> GetAllMovies()
    {
        return _context.Movies.Include(m=>m.Director);
    }

    public Movie GetMovieById(int id)
    {
        var movie = _context.Movies.Include(m => m.Director)
            .FirstOrDefault(m => m.MovieId == id);
        if (movie == null) throw new Exception("Movie not found");
        return movie;
    }

    public Movie GetMovieByTitle(string title)
    {
        var movie = _context.Movies.Include(m => m.Director)
            .FirstOrDefault(m => m.Title == title);
        if (movie == null) throw new Exception("Movie not found");
        return movie;    
    }

    public List<Movie> GetAllMoviesByDirector(int id)
    {
        if(_context.Movies.Any(m => m.Director == null)) throw new Exception("Director not found");
        return _context.Movies.Include(m=>m.Director).Where(m=>m.Director.DirectorId==id).ToList(); 
    }
    
    public async Task<IEnumerable<Movie>> SearchMoviesAsync(string query)
    {
        query = query?.Trim().ToLower();
        if (string.IsNullOrEmpty(query) || query.Length < 3)
            return Enumerable.Empty<Movie>();

        return await _context.Movies
            .Where(m => m.Title.Trim().ToLower().Contains(query))
            .ToListAsync();
    }

    public IQueryable<Movie> SortMoviesAsync(IQueryable<Movie> movies,string sortBy)
    {
        switch (sortBy)
        {
            case "Title": movies=movies.OrderBy(m => m.Title);
                break;
            case "Title Desc.": movies= movies.OrderByDescending(m => m.Title);
                break;
            case "Release Year": movies=movies.OrderBy(m => m.ReleaseYear);
                break;
            case "Release Year Desc.": movies = movies.OrderByDescending(m => m.ReleaseYear);
                break;
            case "Rating": movies=movies.OrderBy(m => m.Rating);
                break;
            case "Rating Desc.": movies = movies.OrderByDescending(m => m.Rating);
                break;
            default: movies=movies.OrderBy(m => m.Title);
                break;
        }
        return movies;
    }
}
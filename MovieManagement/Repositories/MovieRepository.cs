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
    
    public List<Movie> GetAllMovies()
    {
        return _context.Movies.ToList();
    }

    public Movie GetMovieById(int id)
    {
        if(_context.Movies.Any(m=>m.MovieId == id)) return _context.Movies.Find(id);
        throw new Exception("Movie not found");
    }

    public Movie GetMovieByTitle(string title)
    {
        if(_context.Movies.Any(m=>m.Title == title)) return _context.Movies.Find(title);
        throw new Exception("Movie not found");    
    }

    public List<Movie> GetAllMoviesByDirector(int id)
    {
        return _context.Movies.Include(m=>m.Director).Where(m=>m.Director.DirectorId==id).ToList(); 
    }
}
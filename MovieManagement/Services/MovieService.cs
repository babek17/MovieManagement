using MovieManagement.Entities;
using MovieManagement.Repositories;

namespace MovieManagement.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task<IEnumerable<Movie>> SearchAsync(string query)
    {
        return await _movieRepository.SearchMoviesAsync(query);
    }
    
    public IQueryable<Movie> Sort(string sortBy)
    {
        return  _movieRepository.SortMoviesAsync(sortBy);
    }

    public Movie GetMovieById(int movieId)
    {
        return _movieRepository.GetMovieById(movieId);
    }

    public IQueryable<Movie> GetAllMovies()
    {
        return _movieRepository.GetAllMovies();
    }
}
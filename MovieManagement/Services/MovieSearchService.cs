using MovieManagement.Entities;
using MovieManagement.Repositories;

namespace MovieManagement.Services;

public class MovieSearchService: ISearchService<Movie>
{
    private readonly IMovieRepository _movieRepository;

    public MovieSearchService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task<IEnumerable<Movie>> SearchAsync(string query)
    {
        return await _movieRepository.SearchMoviesAsync(query);
    }
}
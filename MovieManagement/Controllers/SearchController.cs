using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Services;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService<Movie> _movieSearchService;
    private readonly ISearchService<Director> _directorSearchService;

    public SearchController(
        ISearchService<Movie> movieSearchService,
        ISearchService<Director> directorSearchService)
    {
        _movieSearchService = movieSearchService;
        _directorSearchService = directorSearchService;
    }

    [HttpGet("movies")]
    public async Task<IActionResult> SearchMovies(string query)
    {
        var results = await _movieSearchService.SearchAsync(query);
        return Ok(results);
    }

    [HttpGet("directors")]
    public async Task<IActionResult> SearchDirectors(string query)
    {
        var results = await _directorSearchService.SearchAsync(query);
        return Ok(results);
    }
}
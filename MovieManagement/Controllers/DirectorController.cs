using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Repositories;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class DirectorController: Controller
{
    private readonly IDirectorRepository _directorRepository;
    private readonly ISearchService<Director> _directorSearchService;

    public DirectorController(IDirectorRepository directorRepository, ISearchService<Director> directorSearchService)
    {
        _directorRepository = directorRepository;
        _directorSearchService = directorSearchService;
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> SearchDirectors([FromQuery] string query)
    {
        var results = await _directorSearchService.SearchAsync(query);

        if (!results.Any())
            return NotFound("No directors matched your query.");

        return Ok(results);
    }
}
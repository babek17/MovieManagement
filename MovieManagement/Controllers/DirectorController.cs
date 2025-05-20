using Microsoft.AspNetCore.Mvc;
using MovieManagement.Entities;
using MovieManagement.Repositories;
using MovieManagement.Services;

namespace MovieManagement.Controllers;

public class DirectorController: Controller
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IDirectorService _directorService;
    public DirectorController(IDirectorRepository directorRepository, IDirectorService directorService)
    {
        _directorRepository = directorRepository;
        _directorService = directorService;
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> SearchDirectors([FromQuery] string query)
    {
        var results = await _directorService.SearchAsync(query);

        if (!results.Any())
            return NotFound("No directors matched your query.");

        return Ok(results);
    }
}
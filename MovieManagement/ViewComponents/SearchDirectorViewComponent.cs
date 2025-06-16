using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Models;
using MovieManagement.Services;

namespace MovieManagement.ViewComponents;

public class SearchDirectorViewComponent: ViewComponent
{
    private readonly IDirectorService _directorService;

    public SearchDirectorViewComponent(IDirectorService directorService)
    {
        _directorService = directorService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(string director)
    {
        var directors = await _directorService.GetAllDirectors()
            .Select(d => new DirectorSearch()
            {
                Id = d.DirectorId,
                Name = $"{d.Name}, {d.Age}"
            })
            .ToListAsync();

        ViewBag.director = director;
        return View(directors);
    }
}
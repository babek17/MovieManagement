using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Repositories;

namespace MovieManagement.ViewComponents;

public class GenreFilterViewComponent : ViewComponent
{
    private readonly MovieManagementDbContext _context;

    public GenreFilterViewComponent(MovieManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync(string genre)
    {
        var genres = await _context.Movies.Select(m=>m.Genre).Distinct().ToListAsync();
        genres.Insert(0, "All Genres");
        ViewBag.Genre = genre;
        return View(genres);
    }
}
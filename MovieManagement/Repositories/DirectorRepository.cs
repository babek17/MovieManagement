using Microsoft.EntityFrameworkCore;
using MovieManagement.Data;
using MovieManagement.Entities;
namespace MovieManagement.Repositories;

public class DirectorRepository: IDirectorRepository
{
    private readonly MovieManagementDbContext _context;

    public DirectorRepository(MovieManagementDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<Director> GetAllDirectors()
    {
        return _context.Directors;
    }

    public Director GetDirectorById(int id)
    {
        if (_context.Directors.Any(d => d.DirectorId == id)) return _context.Directors.Find(id);
        throw new Exception("Director not found");
    }

    public Director GetDirectorByName(string name)
    {
        if (_context.Directors.Any(d => d.Name==name)) return _context.Directors.Find(name);
        throw new Exception("Director not found");    
    }
    
    public async Task<IEnumerable<Director>> SearchDirectorsAsync(string query)
    {
        query = query.Trim();
        if (string.IsNullOrWhiteSpace(query))
            return Enumerable.Empty<Director>();

        query = query.Trim().ToLower();

        return await _context.Directors.Where(d => d.Name.ToLower().Contains(query)).ToListAsync();
    }

}
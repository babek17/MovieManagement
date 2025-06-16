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

    public int GetDirectorIdByName(string name)
    {
        var director = GetDirectorByName(name);
        return director.DirectorId;
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
    
    public IQueryable<Director> SearchDirectors(string search)
    {
        var query = _context.Directors.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(d => d.Name.Contains(search));
        }
        return query;
    }

    public void AddDirector(Director director)
    {
        _context.Directors.Add(director);
        Save();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
    
    public void Remove(int directorId)
    {
        var director = _context.Directors.FirstOrDefault(m => m.DirectorId == directorId);
        if (director == null) throw new Exception("Movie "+directorId+" not found");
        _context.Directors.Remove(director);
        Save();
    }
    
}
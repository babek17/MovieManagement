using MovieManagement.Entities;

namespace MovieManagement.Services;

public interface IDirectorService
{
    Task<IEnumerable<Director>> SearchAsync(string query);
    IQueryable<Director> GetAllDirectors();
}
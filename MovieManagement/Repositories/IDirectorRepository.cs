using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public interface IDirectorRepository
{
    List<Director> GetAllDirectors();
    Director GetDirectorById(int id);
    Director GetDirectorByName(string name);
    Task<IEnumerable<Director>> SearchDirectorsAsync(string query);
}
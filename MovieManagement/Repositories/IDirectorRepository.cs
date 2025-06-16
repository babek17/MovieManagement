using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public interface IDirectorRepository
{
    IQueryable<Director> GetAllDirectors();
    Director GetDirectorById(int id);
    int GetDirectorIdByName(string name);
    Director GetDirectorByName(string name);
    IQueryable<Director> SearchDirectors(string query);
    void AddDirector(Director director);
    void Save();
    void Remove(int directorId);
}
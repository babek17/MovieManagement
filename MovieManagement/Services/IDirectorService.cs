using MovieManagement.Entities;
using MovieManagement.Models;

namespace MovieManagement.Services;

public interface IDirectorService
{
    IQueryable<Director> Search(string query);
    Director GetDirectorByName(string name);
    Director GetDirectorById(int id);
    int GetDirectorIdByName(string name);

    IQueryable<Director> GetAllDirectors();
    int AddDirectorAsync(DirectorToAdd model, string rootPath);
    void RemoveDirector(int directorId);
}
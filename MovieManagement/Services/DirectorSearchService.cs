using MovieManagement.Entities;
using MovieManagement.Repositories;

namespace MovieManagement.Services;

public class DirectorService: IDirectorService
{
    private readonly IDirectorRepository _directorRepository;

    public DirectorService(IDirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }

    public async Task<IEnumerable<Director>> SearchAsync(string query)
    {
        return await _directorRepository.SearchDirectorsAsync(query);
    }
}
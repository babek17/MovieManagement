using MovieManagement.Entities;
using MovieManagement.Models;
using MovieManagement.Repositories;

namespace MovieManagement.Services;

public class DirectorService: IDirectorService
{
    private readonly IDirectorRepository _directorRepository;

    public DirectorService(IDirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }
    
    public Director GetDirectorByName(string name)
    {
        return _directorRepository.GetDirectorByName(name);
    }
    
    public Director GetDirectorById(int id)
    {
        return _directorRepository.GetDirectorById(id);
    }

    public void RemoveDirector(int directorId)
    {
        _directorRepository.Remove(directorId);
    }
    
    public int GetDirectorIdByName(string name)
    {
        return _directorRepository.GetDirectorIdByName(name);
    }

    public IQueryable<Director> Search(string query)
    {
        return _directorRepository.SearchDirectors(query);
    }

    public IQueryable<Director> GetAllDirectors()
    {
        return _directorRepository.GetAllDirectors();
    }
    
    public int AddDirectorAsync(DirectorToAdd model, string rootPath)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        string imagePath = null;

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(rootPath, "wwwroot", "Images", "Directors");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = model.Name.ToLower().Trim().Replace(" ", "_") + Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageFile.CopyToAsync(fileStream);
            }

            imagePath = $"/Images/Directors/{uniqueFileName}";
        }

        var director = new Director
        {
            Name = model.Name,
            DateOfBirth = model.DateOfBirth,
            Bio = model.Bio,
            ImageUrl = imagePath,
            Movies = new List<Movie>()
        };
        
        _directorRepository.AddDirector(director);
        return director.DirectorId;
    }

    
}
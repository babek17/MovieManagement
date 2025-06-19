using MovieManagement.Entities;

namespace MovieManagement.Models;

public class DirectorToAdd
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public IFormFile ImageFile { get; set; }
    public int Age { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Entities;

public class Director
{
    public int DirectorId { get; set; }
    [MaxLength(100)]
    public required string Name { get; set;}
    public int Age { get; set; }
    public string Bio { get; set; }
    public List<Movie> Movies { get; set; }
    public string ImageUrl { get; set; }
}
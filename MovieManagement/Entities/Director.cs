using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieManagement.Attributes;

namespace MovieManagement.Entities;

public class Director
{
    public int DirectorId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [PastDate(ErrorMessage = "Date of birth cannot be in the future.")]
    public DateTime DateOfBirth { get; set; }

    [NotMapped] public int Age => CalculateAge(DateOfBirth);

    [Required]
    [StringLength(2000)]
    public string Bio { get; set; }

    [Required]
    [MaxLength(300)]
    public string ImageUrl { get; set; }

    public List<Movie> Movies { get; set; } = new();
    
    private int CalculateAge(DateTime dob)
    {
        var today = DateTime.Today;
        var age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;
        return age;
    }
}
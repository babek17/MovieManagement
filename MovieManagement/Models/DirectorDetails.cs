using MovieManagement.Entities;

namespace MovieManagement.Models;

public class DirectorDetails
{
    public int DirectorId { get; set; }
    public Director Director { get; set; }
    public IEnumerable<MovieCard> MovieCards { get; set; }
}
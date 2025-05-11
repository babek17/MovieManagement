using Microsoft.AspNetCore.Identity;

namespace MovieManagement.Entities;

public class ApplicationUser: IdentityUser
{
    public int UserId { get; set; }
    public string UniqueUsername { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Movie> Watchlist = new List<Movie>();
    public List<Rating> Ratings { get; set; }
    public List<Comment> Comments { get; set; }
    
}
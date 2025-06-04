using MovieManagement.Entities;
using MovieManagement.Models;

namespace MovieManagement.Services;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserWithWatchlistAsync(string userId);
    Task SaveChangesAsync();
    Task<ApplicationUser?> GetUserByIdAsync(string userId);

}
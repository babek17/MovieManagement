using MovieManagement.Entities;

namespace MovieManagement.Services;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserWithWatchlistAsync(string userId);
    Task SaveChangesAsync();
}
using Microsoft.EntityFrameworkCore;
 using MovieManagement.Entities;
 using MovieManagement.Models;
 using MovieManagement.Repositories;
 
 namespace MovieManagement.Services;
 
 public class UserRepository: IUserRepository
 {
     private readonly MovieManagementDbContext _context;
 
     public UserRepository(MovieManagementDbContext context)
     {
         _context = context;
     }
 
     public async Task<ApplicationUser?> GetUserWithWatchlistAsync(string userId)
     {
         return await _context.Users
             .Include(u => u.Watchlist)
             .ThenInclude(w => w.Movie)
             .FirstOrDefaultAsync(u => u.Id == userId);
     }
 
     public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
     {
         return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
     }
     
     public async Task SaveChangesAsync()
     {
         await _context.SaveChangesAsync();
     }
 }
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;
using MovieManagement.Models;

namespace MovieManagement.Data;

public class MovieManagementDbContext: IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public MovieManagementDbContext(DbContextOptions<MovieManagementDbContext> options): base(options){}
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }
    
    public DbSet<Watchlist> Watchlists { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    
}
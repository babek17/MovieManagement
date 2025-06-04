using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;
using MovieManagement.Models;

namespace MovieManagement.Repositories;

public class MovieManagementDbContext: IdentityDbContext<ApplicationUser>
{
    public MovieManagementDbContext(DbContextOptions<MovieManagementDbContext> options): base(options){}
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }
    
    public DbSet<Watchlist> Watchlists { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Rating> Ratings { get; set; }
    
}
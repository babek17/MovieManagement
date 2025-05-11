using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public class MovieManagementDbContext: IdentityDbContext<ApplicationUser>
{
    public MovieManagementDbContext(DbContextOptions<MovieManagementDbContext> options): base(options){}
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }
}
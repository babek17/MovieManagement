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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Watchlist>()
            .HasKey(w => w.WatchlistId);

        builder.Entity<Watchlist>()
            .HasOne(w => w.User)
            .WithMany(u => u.Watchlist)
            .HasForeignKey(w => w.UserId);

        builder.Entity<Watchlist>()
            .HasOne(w => w.Movie)
            .WithMany(m => m.WatchlistUsers)
            .HasForeignKey(w => w.MovieId);

        // Optionally enforce uniqueness (one movie per user only once)
        builder.Entity<Watchlist>()
            .HasIndex(w => new { w.UserId, w.MovieId })
            .IsUnique();
    }
}
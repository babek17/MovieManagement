using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public class RatingRepository: IRatingRepository
{
    private readonly MovieManagementDbContext _context;

    public RatingRepository(MovieManagementDbContext context)
    {
        _context = context;
    }

    public IQueryable<Rating> GetAllRatings()
    {
        return _context.Ratings;
    }
    public Rating GetRating(string userId, int movieId)
    {
        return _context.Ratings
            .FirstOrDefault(r => r.MovieId == movieId && r.UserId == userId);
    }

    public void AddRating(Rating rating)
    {
        _context.Ratings.Add(rating);
    }

    public void UpdateRating(Rating rating)
    {
        _context.Ratings.Update(rating);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
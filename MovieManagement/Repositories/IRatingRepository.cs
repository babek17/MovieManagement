using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public interface IRatingRepository
{
    Rating GetRating(string userId, int movieId);
    void AddRating(Rating rating);
    void UpdateRating(Rating rating);
    void Save();
    IQueryable<Rating> GetAllRatings();
}
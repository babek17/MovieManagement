using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Entities;

public class Rating
{
    public int RatingId { get; set; }
    public required int MovieId { get; set; }
    public required string UserId { get; set; }
    [Range(0, 10)]
    public required int Score { get; set; }
}
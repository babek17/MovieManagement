namespace MovieManagement.Entities;

public class Rating
{
    public int RatingId { get; set; }
    public Movie Movie { get; set; }
    public ApplicationUser User { get; set; }
    public int Score { get; set; }
}
namespace MovieManagement.Entities;

public class Comment
{
    public int CommentId { get; set; }
    public Movie Movie { get; set; }
    public ApplicationUser User { get; set; }
    public string CommentText { get; set; }
    public DateTime CommentDate { get; set; }
}
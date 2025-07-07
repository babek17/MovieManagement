namespace MovieManagement.Models;

public class AddCommentRequest
{
    public int MovieId { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string CommentText { get; set; }
    public string? ParentCommentId { get; set; }

}
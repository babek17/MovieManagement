namespace MovieManagement.Models;

public class CommentDto
{
    public string Id { get; set; }
    public int MovieId { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string CommentText { get; set; }
    public DateTime CommentDate { get; set; }
    public List<CommentDto> Replies { get; set; } = new();
    public int NestingStage { get; set; } = 0;

}
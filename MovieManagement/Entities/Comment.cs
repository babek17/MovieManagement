using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Entities;

public class Comment
{
    public int CommentId { get; set; }
    [Required]
    public int MovieId { get; set; }
    [Required]
    public ApplicationUser User { get; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    [MaxLength(2500)]
    public string CommentText { get; set; }
    [Required]
    public DateTime CommentDate { get; set; }
}
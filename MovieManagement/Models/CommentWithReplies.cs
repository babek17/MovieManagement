using MovieManagement.Entities;
using MovieManagement.MongoEntities;

namespace MovieManagement.Models;

public class CommentWithReplies
{
    public Comment Parent { get; set; }
    public List<Comment> Replies { get; set; }
}
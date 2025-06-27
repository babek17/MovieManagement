using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieManagement.Entities;
[NotMapped]
public class Comment
{
    [BsonId] // Marks this field as the MongoDB document's _id
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }  // Replaces CommentId

    [BsonElement("movieId")]
    public int MovieId { get; set; }

    [BsonElement("userId")]
    public string UserId { get; set; } // Reference only by UserId

    [BsonElement("userName")]
    public string UserName { get; set; } // Add this to avoid joining with User table

    [BsonElement("commentText")]
    public string CommentText { get; set; }

    [BsonElement("commentDate")]
    public DateTime CommentDate { get; set; }
}
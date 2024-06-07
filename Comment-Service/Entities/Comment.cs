using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Comment_Service.Entities;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
        
    public string? UserId { get; set; }
    public string? PostId { get; set; }
}
namespace Comment_Service.Database;

public class CommentDatabaseSettings : ICommentDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CommentsCollectionName { get; set; } = null!;
}
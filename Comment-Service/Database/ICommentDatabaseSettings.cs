namespace Comment_Service.Database;

public interface ICommentDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CommentsCollectionName { get; set; }
}
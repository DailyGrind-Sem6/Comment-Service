using Comment_Service.Database;
using Comment_Service.Entities;
using MongoDB.Driver;

namespace Comment_Service.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly IMongoCollection<Comment> _comments;

    public CommentRepository(ICommentDatabaseSettings settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.DatabaseName);
        _comments = database.GetCollection<Comment>(settings.CommentsCollectionName);
    }
    
    public async Task<List<Comment>> GetAll()
    {
        return await _comments.Find(comment => true).ToListAsync();
    }
    
    public Task<Comment> Get(string id)
    {
        var filter = Builders<Comment>.Filter.Eq(comment => comment.Id, id);
        var result = _comments.Find(filter).SingleOrDefaultAsync();

        return result;
    }
    
    public async Task<Comment> Create(Comment comment)
    {
        await _comments.InsertOneAsync(comment);
        return comment;
    }
    
    public async Task Update(string id, Comment commentIn)
    {
        var filter = Builders<Comment>.Filter.Eq(comment => comment.Id, id);
        await _comments.ReplaceOneAsync(filter, commentIn);
    }
    
    public async Task Remove(string id)
    {
        await _comments.DeleteOneAsync(comment => comment.Id == id);
    }
}
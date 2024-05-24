using Comment_Service.Entities;

namespace Comment_Service.Services;

public interface ICommentService
{
    public Task<List<Comment>> GetComments();
    public Comment GetCommentById(string id);
    public Task<Comment> CreateComment(Comment comment);
    
    public Task UpdateComment(string id, Comment comment);
    
    public Task RemoveComment(string id);
}
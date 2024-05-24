using Comment_Service.Entities;

namespace Comment_Service.Repositories;

public interface ICommentRepository
{
    public Task<List<Comment>> GetAll();
    public Task<Comment> Get(string id);
    public Task<Comment> Create(Comment comment);
    public Task Update(string id, Comment commentIn);
    public Task Remove(string id);
}
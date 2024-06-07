using Comment_Service.Entities;
using Comment_Service.Repositories;

namespace Comment_Service.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;

    public CommentService(ICommentRepository repository)
    {
        _repository = repository;
    }
    public Task<List<Comment>> GetComments()
    {
        return _repository.GetAll();
    }

    public Comment GetCommentById(string id)
    {
        var comment = _repository.Get(id).Result;
        if (comment == null)
        {
            throw new Exception("Comment not found");
        }

        return comment;
    }

    public Task<Comment> CreateComment(Comment comment)
    {
        return _repository.Create(comment);
    }

    public async Task UpdateComment(string id, Comment comment)
    {
        var existingComment = await _repository.Get(id);

        if (existingComment == null)
        {
            throw new Exception("Comment not found");
        }
        
        comment.Id = existingComment.Id;
        comment.UserId = existingComment.UserId;

        await _repository.Update(id, comment);
    }

    public Task RemoveComment(string id)
    {
        return _repository.Remove(id);
    }
}
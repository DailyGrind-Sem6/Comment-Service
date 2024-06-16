using System.Text.Json;
using Comment_Service.Entities;
using Comment_Service.Kafka;
using Comment_Service.Repositories;

namespace Comment_Service.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;
    private readonly IKafkaProducer _kafkaProducer;

    public CommentService(ICommentRepository repository, IKafkaProducer kafkaProducer)
    {
        _repository = repository;
        _kafkaProducer = kafkaProducer;
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

    public async Task<Comment> CreateComment(Comment comment)
    {
        var createdComment = await _repository.Create(comment);
        
        if (createdComment != null)
        {
            // Convert the comment or the postId to a string format that can be sent to Kafka

            // Send the message to the "comment-created" topic
            await _kafkaProducer.SendMessage(createdComment.PostId, "comment-created");
        }

        return createdComment;
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
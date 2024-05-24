using Comment_Service.Entities;
using Comment_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Comment_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly ICommentService _service;

        public CommentsController(ICommentService service, ILogger<CommentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Console.WriteLine("Getting data from MongoDB...");
            var comments = await _service.GetComments();
            
            return Ok(comments);
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var comment = _service.GetCommentById(id);
                
                return Ok(comment);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            Console.WriteLine("Creating comment...");
            var createdComment = await _service.CreateComment(comment);
            return Ok(createdComment);
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment(string id, [FromBody] Comment comment)
        {
            await _service.UpdateComment(id, comment);
            return Ok();
        }

        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveComment(string id)
        {
            await _service.RemoveComment(id);
            return Ok();
        }
    }
}

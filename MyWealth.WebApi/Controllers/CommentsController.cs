using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWealth.Business.Operations.Comment;
using MyWealth.Business.Operations.Comment.Dtos;
using MyWealth.WebApi.Models;

namespace MyWealth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentRequest request)
        {
            var addCommentdto = new CommentDto
            {
               // Id = request.Id,
                Content = request.Content,
                Title = request.Title,
                StockId = request.StockId,
            };

            var result = await _commentService.AddComment(addCommentdto);

            if(!result.IsSucceed)
                return BadRequest(result.Message);

            else return Ok();
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            var comments = await _commentService.GetAllComments();

            return Ok(comments);
        }

        [HttpPatch("{id}/update")]
        public async Task<IActionResult> UpdateComment(int id, string updatedText)
        {
            var result = await _commentService.UpdateComment(id, updatedText);

            if(!result.IsSucceed)
                return BadRequest(result.Message);

            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteComment(id);

            if(!result.IsSucceed)
                return BadRequest(result.Message);

            return Ok();
        }

        // kullanıcının tüm yorumlarını çek
        
    }
}

using Microsoft.AspNetCore.Authorization;
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
        // dependency injection for comment processes
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // to comment on the stock
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(AddCommentRequest request)
        {
            // To comply with the single responsibility principle, data is transferred via dto
            var addCommentdto = new CommentDto
            {
               UserId = request.UserId,
                Content = request.Content,
                Title = request.Title,
                StockId = request.StockId,
            };

            var result = await _commentService.AddComment(addCommentdto);
            // Checking the result
            if (!result.IsSucceed)
                return BadRequest(result.Message);

            else return Ok();
        }

        // All comments made by user
        [HttpGet("userallcomments")]
        [Authorize]
        public async Task<IActionResult> GetAllComment([FromQuery] StockGetAllCommentRequest request)
        {
            // To comply with the single responsibility principle, data is transferred via dto
            var stockAllComments = new StockGetAllCommentDto
            {
                UserId = request.UserId,
                PageSize=request.PageSize,
                PageNumber=request.PageNumber,
            };

            //It is sent to the comment service for the transactions to be carried out.
            var comments = await _commentService.GetAllComments(stockAllComments);

            return Ok(comments);
        }

        //to update the comment.
        [HttpPatch("{id}/update")]
        public async Task<IActionResult> UpdateComment(int id, string updatedText)
        {
            //It is sent to the comment service for the transactions to be carried out.
            var result = await _commentService.UpdateComment(id, updatedText);

            // Checking the result
            if (!result.IsSucceed)
                return BadRequest(result.Message);

            return Ok();

        }

        // delete comment
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            //It is sent to the comment service for the transactions to be carried out
            var result = await _commentService.DeleteComment(id);

            // Checking the result
            if (!result.IsSucceed)
                return BadRequest(result.Message);

            return Ok();
        }

        
        
    }
}

using MyWealth.Business.Operations.Comment.Dtos;
using MyWealth.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Comment
{
    public interface ICommentService
    {
        public Task<ServiceMessage> AddComment(CommentDto commentDto); // to write a new comment

        public Task<List<AllCommentDto>> GetAllComments(StockGetAllCommentDto stockGetAllComment); // To pull all comments from the user

        public Task<ServiceMessage> UpdateComment(int userid,int stockid,string updatedText); // to update comment

        public Task<ServiceMessage> DeleteComment(int userid, int stockid); // to delete comment
    }
}

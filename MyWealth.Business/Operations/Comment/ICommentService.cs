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
        public Task<ServiceMessage> AddComment(CommentDto commentDto);

        public Task<List<GetCommentDto>> GetAllComments();

        public Task<ServiceMessage> UpdateComment(int id, string updatedText);

        public Task<ServiceMessage> DeleteComment(int id);
    }
}

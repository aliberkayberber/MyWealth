using MyWealth.Business.Operations.Comment.Dtos;
using MyWealth.Business.Types;
using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Comment
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CommentEntity> _repository;
        private readonly IRepository<StockEntity> _stockRepository;

        public CommentManager(IUnitOfWork unitOfWork, IRepository<CommentEntity> repository, IRepository<StockEntity> stockRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _stockRepository = stockRepository;
        }
        public async Task<ServiceMessage> AddComment(CommentDto commentDto)
        {


            var hasStockId = _stockRepository.GetById(commentDto.StockId);

            if (hasStockId is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "No commented stock found"
                };
            }

            var commentEntity = new CommentEntity
            {
               // Id = commentDto.Id,
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = commentDto.StockId,
            };

            _repository.Add(commentEntity);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceMessage
            {
                IsSucceed = true,
            };


        }

        public async Task<ServiceMessage> DeleteComment(int id)
        {
            var comment = _repository.GetById(id);

            if (comment is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Not found comment"
                };
            }

            _repository.Delete(comment);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while Delete the Content");
            }

            return new ServiceMessage
            { IsSucceed = true };
        }

        public async Task<List<GetCommentDto>> GetAllComments()
        {


            var comments = _repository.GetAll()
                                      .Select(x => new GetCommentDto
                                      {
                                          Id = x.Id,
                                          Title = x.Title,
                                          Content = x.Content,
                                          Stock = _stockRepository.GetAllList().Select(y => new CommentStockDto
                                          {
                                              Id = y.Id,
                                              CompanyName = y.CompanyName,
                                              Symbol = y.Symbol
                                          }).FirstOrDefault()
                                      }).ToList();

            return comments;
            

        }

        public async Task<ServiceMessage> UpdateComment(int id, string updatedText)
        {
            var comment = _repository.GetById(id);

            if (comment is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "No comment found matching this id"
                };
            }

            comment.Content = updatedText;
            _repository.Update(comment);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while changing the Content");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }
    }
}

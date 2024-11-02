using Microsoft.EntityFrameworkCore;
using MyWealth.Business.Operations.Comment.Dtos;
using MyWealth.Business.Operations.Portfolio.Dtos;
using MyWealth.Business.Types;
using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Comment
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork; // for database operations
        private readonly IRepository<CommentEntity> _repository; // for comment operations
        private readonly IRepository<StockEntity> _stockRepository; // for stock operations
        private readonly IRepository<UserEntity> _userRepository; // for user operations

        // we do dependency injection.
        public CommentManager(IUnitOfWork unitOfWork, IRepository<CommentEntity> repository, IRepository<StockEntity> stockRepository, IRepository<UserEntity> userRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _stockRepository = stockRepository;
            _userRepository = userRepository;
        }

        //to write a new comment
        public async Task<ServiceMessage> AddComment(CommentDto commentDto)
        {
            var hasUserId = _userRepository.GetById(commentDto.UserId); // user checking

             
            if (hasUserId is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "No commented Username found"
                };
            }

            var hasStockId = _stockRepository.GetById(commentDto.StockId); // stock checking

            if (hasStockId is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "No commented stock found"
                };
            }

            // A new variable is created of type commentEntity
            // To add a comment to the database, it must be of type commentEntity
            var commentEntity = new CommentEntity
            {
                UserId = commentDto.UserId,
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = commentDto.StockId,
                Stock = hasStockId,
                User = hasUserId,
            };

            _repository.Add(commentEntity);
            await _unitOfWork.SaveChangesAsync(); // comments are saved to database

            return new ServiceMessage
            {
                IsSucceed = true,
            };


        }
        // to delete comment
        public async Task<ServiceMessage> DeleteComment(int id)
        {
            var comment = _repository.GetById(id); // It is found by comment id

            if (comment is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Not found comment"
                };
            }

            _repository.Delete(comment); // comment will be deleted.

            try
            {
                await _unitOfWork.SaveChangesAsync(); // changes are transferred to the database
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while Delete the Content"); 
            }

            return new ServiceMessage
            { IsSucceed = true };
        }

        // To pull all comments from the user
        public async Task<List<AllCommentDto>> GetAllComments(StockGetAllCommentDto stockGetAllComment) 
        {
            var user = _userRepository.GetById(stockGetAllComment.UserId); // user is being checked

            // Comments made by the user on the stocks he added to his portfolio
            var comments = _userRepository.GetAll(x => x.Id == user.Id)
                                           .Select(y => new AllCommentDto
                                           {
                                               Username = user.UserName,
                                               Stocks = y.Portfolios.Select(p => new PortfolioStockDto
                                               {
                                                   Id = p.Stock.Id,
                                                   Symbol = p.Stock.Symbol,
                                                   CompanyName = p.Stock.CompanyName,
                                                   Industry = p.Stock.Industry,
                                                   MarketCap = p.Stock.MarketCap,
                                                   Purchase = p.Stock.Purchase,
                                                   LastDiv = p.Stock.LastDiv,
                                                   Comments = y.Comments.Where(w => w.StockId == p.Stock.Id).Select( z => new CommentDto
                                                   {
                                                       UserId = user.Id,
                                                       StockId = z.Stock.Id,
                                                       Title = z.Title,
                                                       Content = z.Content,
                                                   }).ToList(),
                                               }).ToList(),
                                           }).ToList();

            var skipNumber = (stockGetAllComment.PageNumber - 1) * stockGetAllComment.PageSize; // for pagination 

            return comments.Skip(skipNumber).Take(stockGetAllComment.PageSize).ToList(); 
        }

        // to update comment
        public async Task<ServiceMessage> UpdateComment(int id, string updatedText)
        {
            var comment = _repository.GetById(id); // comment found

            if (comment is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "No comment found matching this id"
                };
            }

            comment.Content = updatedText; 

            _repository.Update(comment);  // updated text

            try
            {
                await _unitOfWork.SaveChangesAsync(); // changes are transferred to the database
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

using Microsoft.EntityFrameworkCore;
using MyWealth.Business.Operations.Comment;
using MyWealth.Business.Operations.Comment.Dtos;
using MyWealth.Business.Operations.Stock.Dtos;
using MyWealth.Business.Types;
using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyWealth.Business.Operations.Stock
{
    public class StockManager : IStockService
    {
        private readonly IUnitOfWork _unitOfWork; // for database operations
        private readonly IRepository<StockEntity> _stockRepository; // for stock operations
        private readonly IRepository<PortfolioEntity> _portfolioRepository; // for portfolio operations
        private IRepository<CommentEntity> _commentRepository; //// for comment operations

        // we do dependency injection.
        public StockManager(IUnitOfWork unitOfWork, IRepository<StockEntity> stockRepository, IRepository<PortfolioEntity> portfolioRepository, IRepository<CommentEntity> commentRepository)
        {
            _unitOfWork = unitOfWork;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _commentRepository = commentRepository;
        }

        // adds new stocks
        public async Task<ServiceMessage> AddStock(AddStockDto stock)
        {
            var hasStock = _stockRepository.GetAll(x => x.Symbol.ToLower()  == stock.Symbol.ToLower()).Any(); // stock checking


            if (hasStock)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "This stock already exist"
                };
            }

            var stockEntity = new StockEntity
            {
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,

            };

            _stockRepository.Add(stockEntity); // adding stock
            await _unitOfWork.SaveChangesAsync(); // stock are saved to database

            return new ServiceMessage
            {
                IsSucceed = true,
            };

        }

        // purchase changing
        public async Task<ServiceMessage> AdJustStockPurchase(int id, decimal changeTo)
        {
            var stock = _stockRepository.GetById(id); // stock checking

            if (stock is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "No stocks found matching this id"
                };
            }

            stock.Purchase = changeTo; // changing purchase
            _stockRepository.Update(stock);// update stock
            try
            {
                await _unitOfWork.SaveChangesAsync(); // stock are saved to database
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while changing the price");
            }
            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }

        // delete stock
        public async Task<ServiceMessage> DeleteStock(int id)
        {
            var stock = _stockRepository.GetById(id); // stock checking

            if(stock is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Not Found"
                };
            }

            

            _stockRepository.Delete(id); // delete stock

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Changes transferred to database
            }
            catch (Exception)
            {
                
                throw new Exception("An error occurred while delete");
            }
            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }

        // shows all stocks by pagination
        public async Task<List<StockDto>> GetAllStock(int pagenumber, int pagesize)
        {

            // The features of the stocks and comments about the stock are received.
            var stocks =  _stockRepository.GetAll()
                                         .Select(x => new StockDto
                                         {
                                             Id = x.Id,
                                             Symbol = x.Symbol,
                                             Purchase = x.Purchase,
                                             MarketCap = x.MarketCap,
                                             Industry = x.Industry,
                                             LastDiv = x.LastDiv,
                                             CompanyName = x.CompanyName,
                                             StockComments = x.Comments.Select(c => new StockCommentDto
                                             {
                                                 Id = c.Id,
                                                 Content = c.Content,
                                                 Title = c.Title,
                                             }).ToList()
                                         }).ToList();

            var skipNumber = (pagenumber - 1) * pagesize; // for pagination

            return stocks.Skip(skipNumber).Take(pagesize).ToList(); 
        }
        // shows stock 
        public async Task<StockDto> GetStock(int id)
        {
            // The features of the stock and comments about the stock are received.
            var stock =  _stockRepository.GetAll(x => x.Id == id)
                                         .Select(x => new StockDto
                                         {
                                             Id = x.Id,
                                             Symbol = x.Symbol,
                                             Purchase = x.Purchase,
                                             MarketCap = x.MarketCap,
                                             Industry = x.Industry,
                                             LastDiv = x.LastDiv,
                                             CompanyName = x.CompanyName,
                                             StockComments = x.Comments.Select(c => new StockCommentDto
                                             {
                                                 Id = c.Id,
                                                 Content = c.Content,
                                                 Title = c.Title,
                                             }).ToList()
                                         }).FirstOrDefault();

            return stock;
        }

        // search stock by id
        public async Task<List<StockSearchDto>> SearchById(SearchDto searchDto)
        {
            // The features of the stock and comments about the stock are received.
            var stocks =  _stockRepository.GetAll(x => x.CompanyName.ToLower().Contains(searchDto.CompanyName.ToLower()))
                                         .Select(y => new StockSearchDto
                                         {
                                             Symbol = y.Symbol,
                                             CompanyName = y.CompanyName,
                                             Purchase = y.Purchase,
                                             MarketCap = y.MarketCap,
                                             Industry = y.Industry,
                                             LastDiv = y.LastDiv,
                                         });
            var skipNumber = (searchDto.PageNumber - 1) * searchDto.PageSize;

           

            return stocks.Skip(skipNumber).Take(searchDto.PageSize).ToList(); // Pagination


        }
    }
}

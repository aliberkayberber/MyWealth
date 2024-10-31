using Microsoft.EntityFrameworkCore;
using MyWealth.Business.Operations.Portfolio.Dtos;
using MyWealth.Business.Types;
using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Portfolio
{
    public class PortfolioManager : IPortfolioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<StockEntity> _stockRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<PortfolioEntity> _portfolioRepository;

        public PortfolioManager(IUnitOfWork unitOfWork, IRepository<StockEntity> stockRepository, IRepository<UserEntity> userRepository, IRepository<PortfolioEntity> portfolioRepository)
        {
            _unitOfWork = unitOfWork;
            _stockRepository = stockRepository;
            _userRepository = userRepository;
            _portfolioRepository = portfolioRepository;
        }

        public async Task<ServiceMessage> AddPortfolio(AddPortfolioDto addPortfolioDto)
        {
            var user = _userRepository.Get(x => x.UserName.ToLower() == addPortfolioDto.Username.ToLower());

            if (user is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "User not found"
                };
            }

            await _unitOfWork.BeginTransaction();

            var stock = _stockRepository.Get(x => x.Symbol.ToLower() == addPortfolioDto.Symbol.ToLower());

            var portfolio = new PortfolioEntity
            {
                StockId = stock.Id,
                UserId = user.Id,
            };

            _portfolioRepository.Add(portfolio);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("An error occurred");
            }
            return new ServiceMessage
            {
                IsSucceed = true,
            };

        }
        
        public async Task<List<PortfolioDto>> GetUserPortfolio(string UserName)
        {
            var user =  _userRepository.Get(x => x.UserName.ToLower() == UserName.ToLower());

            var portfolio = await _userRepository.GetAll(x => x.Id == user.Id).Select(u => new PortfolioDto
            {
                Username = user.UserName,
                Stocks = u.Portfolios.Select(p => new PortfolioStockDto
                {
                    Id = p.Stock.Id,
                    Symbol = p.Stock.Symbol,
                    CompanyName = p.Stock.CompanyName,
                    Industry = p.Stock.Industry,
                    MarketCap = p.Stock.MarketCap,
                    Purchase = p.Stock.Purchase,
                    LastDiv = p.Stock.LastDiv

                }).ToList(),
            }).ToListAsync();

            return portfolio;

            
        }
        
    }
}

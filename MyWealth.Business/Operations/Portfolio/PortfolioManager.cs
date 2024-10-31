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

        public async Task<ServiceMessage> DeletePortfolio(DeletePortfolioDto deletePortfolio)
        {
            var user = _userRepository.Get(x => x.UserName.ToLower() == deletePortfolio.Username.ToLower());


            if (user is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "User not found"
                };
            }
            var stock = _stockRepository.Get(x => x.Symbol.ToLower() == deletePortfolio.Symbol.ToLower());



            var portfolio = _portfolioRepository.Get(x => x.StockId == stock.Id && x.UserId == user.Id);

            if (portfolio is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Portfolio not found"
                };
            }
            _portfolioRepository.Delete(portfolio,false);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw new Exception("An error occurred");
            }

            return new ServiceMessage
            {
                IsSucceed= true,
            };

        }

        public async Task<ServiceMessage> PatchPortfolio(PatchPortfolioDto portfolioDto)
        {
            var user = _userRepository.Get(x => x.UserName.ToLower() == portfolioDto.Username.ToLower());

            if (user is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "User not found"
                };
            }

            var changingStock = _stockRepository.Get(x => x.Symbol.ToLower() == portfolioDto.Changing.ToLower()); // Share to be exchanged

            if(changingStock is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Stock not found"
                };
            }

            var changeToStock = _stockRepository.Get(x => x.Symbol.ToLower() == portfolioDto.ChangeToSymbol.ToLower());

            if (changeToStock is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Stock not found"
                };
            }

            var portfolio = _portfolioRepository.Get(x => x.StockId == changingStock.Id && x.UserId == user.Id);

            if (portfolio is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Portfolio not found"
                };
            }

            await _unitOfWork.BeginTransaction();

            

            _portfolioRepository.Delete(portfolio, false);
            

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("An error occurred when delete");
            }

            var newPortfolio = new PortfolioEntity
            {
                StockId = changeToStock.Id,
                UserId = user.Id,
            };

            _portfolioRepository.Add(newPortfolio);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                 await _unitOfWork.CommitTransaction();
            }
            catch(Exception)
            {
                throw new Exception("An error occurred when add");
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

        public async Task<ServiceMessage> UpdatePortfolio(UpdatePortfolioDto portfolioDto)
        {
            var user = _userRepository.Get(x => x.UserName.ToLower() == portfolioDto.Username.ToLower());

            if (user is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "User not found"
                };
            }

            var portfolio = _portfolioRepository.GetAll(x => x.UserId == user.Id).ToList();

            if (portfolio is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "portfolio not found"
                };
            }

             await _unitOfWork.BeginTransaction();

            foreach (var item in portfolio)
            {
                _portfolioRepository.Delete(item, false);
            }
            

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception )
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("An error occurred when delete");
            }

            foreach (var stockId in portfolioDto.StockIds)
            {
                var newPortfolio = new PortfolioEntity
                {
                    UserId = user.Id,
                    StockId = stockId,
                };

                _portfolioRepository.Add(newPortfolio);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch(Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("An error occurred when Update");

            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };


        }
    }
}

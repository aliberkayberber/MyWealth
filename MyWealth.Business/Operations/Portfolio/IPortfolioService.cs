using MyWealth.Business.Operations.Portfolio.Dtos;
using MyWealth.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Portfolio
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDto>> GetUserPortfolio(string UserName); // pulls user's portfolio

        Task<ServiceMessage> AddPortfolio(AddPortfolioDto addPortfolioDto); // adds new stocks to the user's portfolio

        Task<ServiceMessage> DeletePortfolio(DeletePortfolioDto deletePortfolioDto);  // deletes a stock from the user's portfolio

        Task<ServiceMessage> PatchPortfolio(PatchPortfolioDto portfolioDto);  // swaps one stock from the user's portfolio for another

        Task<ServiceMessage> UpdatePortfolio(UpdatePortfolioDto portfolioDto); // Deletes all stocks from the user's portfolio and replaces them with all the given stocks
    }
}

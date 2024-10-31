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
        Task<List<PortfolioDto>> GetUserPortfolio(string UserName);

        Task<ServiceMessage> AddPortfolio(AddPortfolioDto addPortfolioDto);

        Task<ServiceMessage> DeletePortfolio(DeletePortfolioDto deletePortfolioDto);

        Task<ServiceMessage> PatchPortfolio(PatchPortfolioDto portfolioDto);

        Task<ServiceMessage> UpdatePortfolio(UpdatePortfolioDto portfolioDto);
    }
}

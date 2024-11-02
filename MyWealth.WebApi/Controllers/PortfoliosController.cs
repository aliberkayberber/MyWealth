using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWealth.Business.Operations.Portfolio;
using MyWealth.Business.Operations.Portfolio.Dtos;
using MyWealth.WebApi.Models;

namespace MyWealth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        // dependency injection for portfolio processes
        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        // pulls user's portfolio
        [HttpGet("Username")]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio(string Username)
        {
            //It is sent to the portfolio service for the transactions to be carried out.
            var result = await _portfolioService.GetUserPortfolio(Username);

            if (result == null)
            {
                return NotFound("Portfolio not found");
            }
            else
                return Ok(result);

        }
        
        [HttpPost("username")]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(AddPortfolioRequest request)
        {
            // To comply with the single responsibility principle, data is transferred via dto
            var addPortfolioDto = new AddPortfolioDto
            {
               Username = request.Username,
               Symbol = request.Symbol,
            };

            //It is sent to the portfolio service for the transactions to be carried out.
            var result = await _portfolioService.AddPortfolio(addPortfolioDto);

            if(!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
                return Ok();

        }

        // Deletes the given stock from the portfolio
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(DeletePortfolioRequest request)
        {
            // To comply with the single responsibility principle, data is transferred via dto
            var deleteDto = new DeletePortfolioDto
            {
                Username = request.Username,
                Symbol = request.Symbol,
            };

            //It is sent to the portfolio service for the transactions to be carried out.
            var result = await _portfolioService.DeletePortfolio(deleteDto);

            if(!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else return Ok();

        }

        // change a stock
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchPortfolio(PatchPortfolioRequest request)
        {
            // To comply with the single responsibility principle, data is transferred via dto
            var patchPortfolioDto = new PatchPortfolioDto
            {
                Username= request.Username,
                Changing = request.Changing,
                ChangeToSymbol= request.ChangeToSymbol,
            };

            //It is sent to the portfolio service for the transactions to be carried out.
            var result = await _portfolioService.PatchPortfolio(patchPortfolioDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }

            else return Ok();

        }

        // add and delete multiple stocks
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePortfolio(UpdatePortfolioRequest request)
        {
            // To comply with the single responsibility principle, data is transferred via dto
            var updatePortfolioDto = new UpdatePortfolioDto
            {
                Username = request.UserName,
                StockIds = request.StockIds,
            };

            //It is sent to the portfolio service for the transactions to be carried out.
            var result = await _portfolioService.UpdatePortfolio(updatePortfolioDto);

            if(!result.IsSucceed)
            { return BadRequest(result.Message); }
            else return Ok();

        }
    }
}

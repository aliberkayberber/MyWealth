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

        public PortfoliosController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        

        [HttpGet("Username")]
        public async Task<IActionResult> GetUserPortfolio(string Username)
        {
            var result = await _portfolioService.GetUserPortfolio(Username);

            if (result == null)
            {
                return NotFound("Portfolio not found");
            }
            else
                return Ok(result);

        }
        
        [HttpPost("username")]
        public async Task<IActionResult> AddPortfolio(AddPortfolioRequest request)
        {
            var addPortfolioDto = new AddPortfolioDto
            {
               Username = request.Username,
               Symbol = request.Symbol,
            };

            var result = await _portfolioService.AddPortfolio(addPortfolioDto);

            if(!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
                return Ok();

        }

        // HttpDelete => verilen  stock u siler portfoliodan

        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio(DeletePortfolioRequest request)
        {
            var deleteDto = new DeletePortfolioDto
            {
                Username = request.Username,
                Symbol = request.Symbol,
            };

            var result = await _portfolioService.DeletePortfolio(deleteDto);

            if(!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else return Ok();

        }

        // httpPatch => bir stock değiştir

        [HttpPatch]
        public async Task<IActionResult> PatchPortfolio(PatchPortfolioRequest request)
        {

            var patchPortfolioDto = new PatchPortfolioDto
            {
                Username= request.Username,
                Changing = request.Changing,
                ChangeToSymbol= request.ChangeToSymbol,
            };

            var result = await _portfolioService.PatchPortfolio(patchPortfolioDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }

            else return Ok();

        }

        // http put => birden fazla stock u ekle ve sil
        [HttpPut]
        public async Task<IActionResult> UpdatePortfolio(UpdatePortfolioRequest request)
        {
            var updatePortfolioDto = new UpdatePortfolioDto
            {
                Username = request.UserName,
                StockIds = request.StockIds,
            };


            var result = await _portfolioService.UpdatePortfolio(updatePortfolioDto);

            if(!result.IsSucceed)
            { return BadRequest(result.Message); }
            else return Ok();

        }
    }
}

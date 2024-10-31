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
                return NotFound();
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
        
       


    }
}

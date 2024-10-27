using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWealth.Business.Operations.Stock;
using MyWealth.Business.Operations.Stock.Dtos;
using MyWealth.WebApi.Models;

namespace MyWealth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var hotels = await _stockService.GetAllStock();
            return Ok(hotels);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int id)
        { 
            var hotel = await _stockService.GetStock(id);
            if (hotel == null)
                return NotFound();
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockRequest request)
        {
            var addStockDto = new AddStockDto
            {
                Symbol = request.Symbol,
                CompanyName = request.CompanyName,
                Purchase=request.Purchase,
                LastDiv=request.LastDiv,
                MarketCap=request.MarketCap,
                Industry=request.Industry,
               //CommentIds=request.CommentIds,
            };

            var result = await _stockService.AddStock(addStockDto);

            if(!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
                return Ok(result);
        }

        [HttpPatch("{id}/purchase")]
        public async Task<IActionResult> AdJustStockPurchase(int id, decimal changeTo)
        {
            var result = await _stockService.AdJustStockPurchase(id, changeTo);

            if(!result.IsSucceed) 
                return NotFound(result.Message);

            else return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var result = await _stockService.DeleteStock(id);

            if(!result.IsSucceed)
                return NotFound(result.Message);

            else
                return Ok();
        }


        
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWealth.Business.Operations.Stock;
using MyWealth.Business.Operations.Stock.Dtos;
using MyWealth.WebApi.Filters;
using MyWealth.WebApi.Models;

namespace MyWealth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        // dependency injection for portfolio processes
        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        // shows all stocks by pagination
        [HttpGet("{pagenumber}/{pagesize}")]
        public async Task<IActionResult> GetAllStocks(int pagenumber, int pagesize)
        {
            //It is sent to the stock service for the transactions to be carried out.
            var hotels = await _stockService.GetAllStock(pagenumber,pagesize);
            return Ok(hotels);
        }

        // shows stock by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int id)
        {
            //It is sent to the stock service for the transactions to be carried out.
            var hotel = await _stockService.GetStock(id);

            if (hotel == null)
                return NotFound("Not found");
            return Ok(hotel);
        }

        // search stock by id
        [HttpGet("search")]
        public async Task<IActionResult> SearchStock([FromQuery] SearchStockQuery query )
        {
            if(string.IsNullOrWhiteSpace(query.CompanyName))
                return NotFound();

            // To comply with the single responsibility principle, data is transferred via dto
            var searchDto = new SearchDto
            {
                CompanyName = query.CompanyName,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
            };

            //It is sent to the stock service for the transactions to be carried out.
            var stocks = await _stockService.SearchById(searchDto);

            if (stocks is null)
                return NotFound("Not found");

            return Ok(stocks);
        }

        // adds new stocks
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> AddStock(AddStockRequest request)
        {
            // To comply with the single responsibility principle, data is transferred via dto
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

            //It is sent to the stock service for the transactions to be carried out.
            var result = await _stockService.AddStock(addStockDto);

            if(!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
                return Ok(result);
        }

        // purchase changing
        [HttpPatch("{id}/purchase")]
        public async Task<IActionResult> AdJustStockPurchase(int id, decimal changeTo)
        {
            //It is sent to the stock service for the transactions to be carried out.
            var result = await _stockService.AdJustStockPurchase(id, changeTo);

            if(!result.IsSucceed) 
                return NotFound(result.Message);

            else return Ok(result);
        }

        // delete stock by id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            //It is sent to the stock service for the transactions to be carried out.
            var result = await _stockService.DeleteStock(id);

            if(!result.IsSucceed)
                return NotFound(result.Message);

            else
                return Ok();
        }


        
    }
}

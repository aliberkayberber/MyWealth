using MyWealth.Business.Operations.Stock.Dtos;
using MyWealth.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Stock
{
    public interface IStockService
    {
        public Task<List<StockDto>> GetAllStock(int pagenumber,int pagesize); // shows all stocks by pagination

        public Task<StockDto> GetStock(int id); // shows stock 

        public Task<ServiceMessage> AddStock(AddStockDto stock); // adds new stocks

        public Task<ServiceMessage> AdJustStockPurchase(int id, decimal changeTo); // purchase changing

        public Task<ServiceMessage> DeleteStock(int id); // delete stock by id

        public Task<List<StockSearchDto>> SearchById(SearchDto searchDto); // search stock by id
    }
}

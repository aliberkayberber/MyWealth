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
        public Task<List<StockDto>> GetAllStock();

        public Task<StockDto> GetStock(int id);

        public Task<ServiceMessage> AddStock(AddStockDto stock);

        public Task<ServiceMessage> AdJustStockPurchase(int id, decimal changeTo);

        public Task<ServiceMessage> DeleteStock(int id);

        public Task<List<StockSearchDto>> SearchById(SearchDto searchDto);
    }
}

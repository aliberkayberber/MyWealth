using MyWealth.Business.Operations.Portfolio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Comment.Dtos
{
    public class AllCommentDto
    {
        public string Username { get; set; }
        public List<PortfolioStockDto> Stocks { get; set; }

        //public List<CommentDto> Comments { get; set; }
    }
}

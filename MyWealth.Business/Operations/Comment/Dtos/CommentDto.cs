using MyWealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Comment.Dtos
{
    public class CommentDto
    {
        //public int Id { get; set; }

        public int StockId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;


    }
}

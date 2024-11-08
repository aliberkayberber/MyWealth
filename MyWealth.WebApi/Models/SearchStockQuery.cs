﻿using System.ComponentModel.DataAnnotations;

namespace MyWealth.WebApi.Models
{
    public class SearchStockQuery
    {
        [MinLength(3, ErrorMessage = "Title must be 3 characters")]
        public string? CompanyName { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoencoPOS.Models.ViewModels
{
    public class SalesReportViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal QuantitySold { get; set; }
    }
}
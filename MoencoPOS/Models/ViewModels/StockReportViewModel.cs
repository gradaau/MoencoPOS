using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoencoPOS.Models.ViewModels
{
    public class StockReportViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitCost { get; set; }
        public decimal QuantityOnHand { get; set; }

    }
}
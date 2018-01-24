using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoencoPOS.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Quantity must be greater than zero(0).")]
        public int Quantity { get; set; }

        public virtual Productc Product { get; set; }

        public virtual Branch Branch { get; set; }
    }
}

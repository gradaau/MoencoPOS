using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class ProductTransferLineItem
    {
        public int ProductTransferLineItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Quantity must be greater than zero(0).")]
        public int Quantity { get; set; }

        public int ProductTransferId { get; set; }
        public ProductTransfer ProductTransfer { get; set; }
    }
}

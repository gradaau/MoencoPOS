using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class SalesLineItem
    {
        public int SalesLineItemId { get; set; }
        [Required]
        public int SalesInvoiceId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Quantity must be greater than zero(0).")]
        public Decimal Quantity { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Price must be greater than zero(0).")]
        public Decimal UnitPrice { get; set; }
        public virtual SalesInvoice SalesInvoice { get; set; }
        public virtual Productc Productc { get; set; }
    }
}

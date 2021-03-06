﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoencoPOS.Models
{
    public class ProductReceiveLineItem
    {
        public int ProductReceiveLineItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Quantity must be greater than zero(0).")]
        public int Quantity { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Cost must be greater than zero(0).")]
        [DisplayName("Unit Cost")]
        public decimal UnitCost { get; set; }

        public int ProductReceiveId { get; set; }
        public ProductReceive ProductReceive { get; set; }
    }
}

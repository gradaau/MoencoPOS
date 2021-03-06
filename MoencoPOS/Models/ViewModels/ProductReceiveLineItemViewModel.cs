﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoencoPOS.Models.ViewModels
{
    public class ProductReceiveLineItemViewModel
    {
        public int ProductReceiveLineItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Display(Name = "Product name")]
        public string Productname { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Range(1, 10000000)]
        [DisplayName("Unit Cost")]
        public decimal UnitCost { get; set; }

        public int ProductReceiveId { get; set; }
        public ProductReceive ProductReceive { get; set; }
    }
}
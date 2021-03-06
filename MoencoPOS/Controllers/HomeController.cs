﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using MoencoPOS.Security;
using MoencoPOS.DAL;
using MoencoPos.Sales.Services;
using MoencoPOS.Models;
using MoencoPOS.Models.ViewModels;
using MoencoPos.Product.Services;
using MoencoPos.Store.Services;

namespace MoencoPOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISalesInvoiceService _salesInvoiceService;
        private readonly IProductService _productService;
        private readonly IProductReceiveService _productReceiveService;
        private readonly IProductTransferService _productTransferService;
        private readonly IBranchService _branchService;

        public HomeController(ISalesInvoiceService salesInvoiceService, IProductService productService,
            IProductReceiveService productReceiveService, IProductTransferService productTransferService,
            IBranchService branchService)
        {
            _salesInvoiceService = salesInvoiceService;
            _productService = productService;
            _productReceiveService = productReceiveService;
            _productTransferService = productTransferService;
            _branchService = branchService;
        }

        [Authorize]
        public ActionResult Index()
        {
            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            UserManager<MyIdentityUser> userManager = new UserManager<MyIdentityUser>(userStore);

            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);

            //MoencoPOSContext northwindDb = new MoencoPOSContext();
            //List<AspNetUsers> model = null;

            //if (userManager.IsInRole(user.Id, "Administrator"))
            //{
            //    model = northwindDb.Customers.ToList();
            //}

            //if (userManager.IsInRole(user.Id, "Operator"))
            //{
            //    model = northwindDb.Customers.Where(c => c.Country == "USA").ToList();
            //}
            var salesInvoices = new List<SalesInvoice>();
            if (user.Role == "Administrator")
            {
                salesInvoices = _salesInvoiceService.Get(t=>t.Status == "Paid", null, "SalesLineItems").OrderByDescending(o=>o.DateSold).ToList();
            }
            else
            {
                salesInvoices = _salesInvoiceService.Get(t => t.BranchId == user.BranchId && t.Status == "Paid", null, "SalesLineItems").OrderByDescending(o => o.DateSold).ToList();
            }

            var salesLineItems = new List<SalesLineItem>();
            foreach (var salesInvoice in salesInvoices)
            {
                foreach (var sLI in salesInvoice.SalesLineItems)
                {
                    salesLineItems.Add(sLI);
                }
            }
            var groupedSLIs = (from tr in salesLineItems
                               group tr by new { tr.ProductId }
                              into groupedSLI
                               select
                              new
                              {
                                  ProductID = groupedSLI.Key.ProductId,
                                  ProductName = _productService.FindById(groupedSLI.Key.ProductId).ProductName,
                                  UnitPrice = _productService.FindById(groupedSLI.Key.ProductId).UnitPrice,
                                  QuantitySold = groupedSLI.Sum(p => p.Quantity)
                              });
            List<SalesReportViewModel> salesReportViewModels = (from groupedSLI in groupedSLIs let i = groupedSLI where i != null where i != null select new SalesReportViewModel() { ProductId = i.ProductID, ProductName = i.ProductName, QuantitySold = i.QuantitySold, UnitPrice = i.UnitPrice }).ToList();
            ViewBag.GroupedSLIs = salesReportViewModels.Take(10);

            var allSalesInvoices = _salesInvoiceService.Get(t => t.Status == "Returned", null, "SalesLineItems").OrderByDescending(o => o.DateSold).ToList();
            var salesInvoiceViewModels = new List<SalesInvoiceViewModel>();
            foreach (var salesInvoice in allSalesInvoices)
            {
                var salesInvoiceViewModel = new SalesInvoiceViewModel()
                {
                    BranchId = salesInvoice.BranchId,
                    CustomerId = salesInvoice.CustomerId,
                    CustomerName = salesInvoice.Customer.FirstName + " " + salesInvoice.Customer.LastName,
                    BranchName = salesInvoice.Branch.BranchName,
                    DateSold = salesInvoice.DateSold,
                    ReferenceNo = salesInvoice.ReferenceNo,
                    SalesInvoiceId = salesInvoice.SalesInvoiceId,
                    SalesType = salesInvoice.SalesType,
                    UserId = salesInvoice.UserId,
                    UserName = userManager.FindById(salesInvoice.UserId).FullName,
                    Status = salesInvoice.Status
                };
                salesInvoiceViewModels.Add(salesInvoiceViewModel);
            }
            ViewBag.ReturnedSales = salesInvoiceViewModels.Take(10);

            var productReceives = _productReceiveService.GetAllProductReceive();
            var productReceivesViewModels = new List<ProductReceiveViewModel>();
            foreach (var productReceive in productReceives)
            {
                var productReceiveViewModel = new ProductReceiveViewModel()
                {
                    BranchId = productReceive.BranchId,
                    BranchName = productReceive.Branch.BranchName,
                    DateReceived = productReceive.DateReceived,
                    ProductReceiveId = productReceive.ProductReceiveId,

                    UserId = productReceive.UserId,
                    UserName = userManager.FindById(productReceive.UserId).FullName
                };
                productReceivesViewModels.Add(productReceiveViewModel);
            }
            ViewBag.ReceivedStock = salesInvoiceViewModels.Take(10);

            var productTransfers = _productTransferService.GetAllProductTransfer();
            var producttransferViewModels = new List<ProductTransferViewModel>();
            foreach (var productTransfer in productTransfers)
            {
                var productTransferViewModel = new ProductTransferViewModel()
                {
                    FromBranchId = productTransfer.FromBranchId,
                    ToBranchId = productTransfer.ToBranchId,
                    ProductTransferId = productTransfer.ProductTransferId,
                    DateTransfered = productTransfer.DateTransfered,
                    UserId = productTransfer.UserId,
                    UserName = userManager.FindById(productTransfer.UserId).FullName
                };
                productTransferViewModel.FromBranchName = _branchService.FindById(productTransferViewModel.FromBranchId).BranchName;
                productTransferViewModel.ToBranchName = _branchService.FindById(productTransferViewModel.ToBranchId).BranchName;

                producttransferViewModels.Add(productTransferViewModel);

            }
            ViewBag.TransferredStock = salesInvoiceViewModels.Take(10);

            if (user!=null)
                ViewBag.FullName = user.FullName;
            else
                ViewBag.FullName = "Admin";
            //return View(model);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
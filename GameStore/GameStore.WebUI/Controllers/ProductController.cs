using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Abstructs;
using GameStore.Domain.Domain;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        
   

        private IProductRepository productRoposit;
        public int pageSize = 4;
        public ProductController(IProductRepository products)
        {
            this.productRoposit = products;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = productRoposit.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p=>p.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                pagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    productRoposit.Products.Count()
                    :
                    productRoposit.Products.Where(p => p.Category == category).Count()
                    
                },
                CurrentCategory = category
            };

            return View(model);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Abstructs;
using GameStore.Domain.Domain;

namespace GameStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository rository;
        public AdminController(IProductRepository repostor)
        {
            this.rository = repostor;
        }

        public ViewResult Index()
        {
            return View(rository.Products);
        }
        public ViewResult Edit (int productId)
        {
            Product product = rository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                rository.SaveProduct(product);
                TempData["message"] = string.Format("{0} Has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deleteproduct = rository.DeleteItem(productId);
            if (deleteproduct != null)
            {
                TempData["message"] = string.Format("{0} deleted was sucsesful", deleteproduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Abstructs;



namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repositors)
        {
            this.repository = repositors;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> colectionCategory = repository.Products
                .Select(m => m.Category)
                .Distinct()
                .OrderBy(x => x);


            return PartialView(colectionCategory);
        }
    }
}

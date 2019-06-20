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
    public class CartController : Controller
    {
        private IProductRepository repository;
        public CartController(IProductRepository reposit)
        {
            repository = reposit;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { 
                cart = cart,
                //GetCart(),
                returnUrl = returnUrl
             
            });
        }
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if(productId != null)
            {
                cart.AddItem(product, 1);
                //GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl});
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if(productId != null)
            {
                GetCart().RemoveItem(product);
               // cart.RemoveItem(product);
            }

            return RedirectToAction("Index", new { returnUrl});
        }
        private Cart GetCart()
        {
            Cart cart =(Cart)Session["Cart"];
            if(cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Domain;

namespace GameStore.WebUI.Binders
{
    public class CartModelBinder: IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContex, ModelBindingContext bindingsContex)
        {
            Cart cart = (Cart)controllerContex.HttpContext.Session[sessionKey];
            if(cart == null)
            {
                cart = new Cart();
                controllerContex.HttpContext.Session[sessionKey] = cart;
            }
            return cart;
        }
    }
}
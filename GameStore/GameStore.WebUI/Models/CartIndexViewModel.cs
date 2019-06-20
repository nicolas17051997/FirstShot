using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Domain.Domain;

namespace GameStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart cart { get; set; }
        public string returnUrl { get; set; }
    }
}
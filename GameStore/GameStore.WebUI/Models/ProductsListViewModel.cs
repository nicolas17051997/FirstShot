using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Domain.Domain;

namespace GameStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set;}
        public PagingInfo pagingInfo { get; set;}
        public string CurrentCategory { get; set; }
    }
}
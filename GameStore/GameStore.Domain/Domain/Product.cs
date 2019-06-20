using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Domain
{
   public class Product
    {
       [HiddenInput (DisplayValue = false)]
        public int ProductID { get; set; }
       [Required (ErrorMessage = "Pleace enter a product name")]
        public string Name { get; set; }
       [DataType (DataType.MultilineText)]
       [Required(ErrorMessage = "Pleace enter a description of product")]
        public string Description { get; set; }
       [Required]
       [Range (0.01, double.MaxValue, ErrorMessage = "Pleace enter a positive value")]
        public decimal Price { get; set; }
       [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }
    }
}

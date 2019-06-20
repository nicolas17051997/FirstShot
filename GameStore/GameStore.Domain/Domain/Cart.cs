using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain;

namespace GameStore.Domain.Domain
{
   public class Cart
    {
       private List<CartLine> lineCollection = new List<CartLine>();

       public void AddItem (Product product, int quantity)
       {
           CartLine line = lineCollection
               .Where(p => p.productProp.ProductID == product.ProductID)
               .FirstOrDefault();
           if(line == null)
           {
               lineCollection.Add(new CartLine
               {
                   productProp = product,
                   quantity = quantity
               });
           }
           else { line.quantity += quantity; }
       }
       public void RemoveItem(Product product)
       {
           lineCollection.RemoveAll(I => I.productProp.ProductID == product.ProductID);

       }
       public decimal TotalValue()
       {
           return lineCollection.Sum(s => s.productProp.Price * s.quantity);
       }
       public void Cleare()
       {
           lineCollection.Clear();
       }
       public IEnumerable<CartLine> Lines
       {
           get {return lineCollection;}
       }


    }
    public class CartLine
    {
        public Product productProp { get; set; }
        public int quantity { get; set; }
    }
}

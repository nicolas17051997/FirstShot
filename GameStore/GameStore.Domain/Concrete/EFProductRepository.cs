using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Concrete;
using GameStore.Domain.Abstructs;
using GameStore.Domain.Domain;

namespace GameStore.Domain.Concrete
{
   public class EFProductRepository : IProductRepository
    {
        private EFDbContext dbContext = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return dbContext.Products; }
        }
       public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                dbContext.Products.Add(product);
            }
            else
            {
                Product DBEntity = dbContext.Products.Find(product.ProductID);
                if(DBEntity != null)
                {
                    DBEntity.Name = product.Name;
                    DBEntity.Description = product.Description;
                    DBEntity.Category = product.Category;
                    DBEntity.Price = product.Price;
                }
            }
            dbContext.SaveChanges();
        }
       public Product DeleteItem(int productID)
       {
           Product DbEntity = dbContext.Products.Find(productID);
           if(DbEntity != null)
           {
               dbContext.Products.Remove(DbEntity);
               dbContext.SaveChanges();

           }
           return DbEntity;
       }
    }
}

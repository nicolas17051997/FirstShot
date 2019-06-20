using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Domain;

namespace GameStore.Domain.Abstructs
{
   public interface IProductRepository
    {
       IEnumerable<Product> Products { get; }
       void SaveProduct (Product Product);
       Product DeleteItem(int productID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GameStore.Domain.Concrete;
using GameStore.Domain.Domain;
namespace GameStore.Domain.Initialize
{
   public class InitializeDbProduct : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            context.Products.Add(new Product { Name = "Kayak", Description = "A board for one persone", Category = "Watersports", Price = 214m });
            context.Products.Add(new Product { Name = "Soccer Ball", Description = "FIFA-approved size and weigth", Category = "Soccer", Price = 19m });
            context.Products.Add(new Product { Name = "Thinking Cap", Description = "Improve your brain efficiency by 75%", Category = "Chess", Price = 16m });
            context.Products.Add(new Product { Name = "Billy Tiny", Description = "Inhhhhhh", Category = "Fish", Price = 214755m });
            context.Products.Add(new Product { Name = "Orbiter", Description = "Circle for bikes", Category = "Bike", Price = 2145m });

            base.Seed(context);
        }
    }
}

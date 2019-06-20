using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstructs;
using GameStore.Domain.Domain;
using GameStore.WebUI.Controllers;

namespace GameStore.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product [] {
                new Product{ ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            List<Product> result = ((IEnumerable<Product>)controller.Index().Model).ToList();

            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(result[0].Name, "P1");
            Assert.AreEqual(result[3].ProductID, 4);
        }
        [TestMethod]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
                new Product{ ProductID = 1, Name = "p2"},
                new Product{ ProductID = 2, Name = "p3"},
                new Product{ ProductID = 3, Name = "p5"}
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            Product p1 = controller.Edit(3).ViewData.Model as Product;
            Product p2 = controller.Edit(1).ViewData.Model as Product;

            Assert.AreEqual(3, p1.ProductID);
            Assert.AreEqual(1, p2.ProductID);

        }
    }
}

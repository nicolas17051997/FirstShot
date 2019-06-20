using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Domain;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using System.Web.Mvc;
using Moq;
using GameStore.Domain.Abstructs;

namespace GameStore.UnitTests
{
    [TestClass]
   public class CartTest
    {
    [TestMethod]
        public void Can_Add_To_Cart()
        {
        //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]{
                new Product{ ProductID = 1, Name = "P1", Category = "Balls"}
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object);


            controller.AddToCart(cart, 1, null);

        //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].productProp.Name, "P1");
            Assert.AreEqual(cart.Lines.ToArray()[0].productProp.ProductID, 1);
        }
        [TestMethod]
    public void Adding_Product_To_Cart()
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        mock.Setup(p => p.Products).Returns(new Product[]{
                new Product{ ProductID = 1, Name = "P1", Category = "Balls"}
            }.AsQueryable());

        Cart cart = new Cart();
        CartController controller = new CartController(mock.Object);

        RedirectToRouteResult routResult = controller.AddToCart(cart, 2, "MyUrl");

        Assert.AreEqual(routResult.RouteValues["action"],"Index");
        Assert.AreEqual(routResult.RouteValues["returnUrl"], "MyUrl");
    }
        [TestMethod]
    public void Can_View_Cart_Contents()
    {

    }
    }
}

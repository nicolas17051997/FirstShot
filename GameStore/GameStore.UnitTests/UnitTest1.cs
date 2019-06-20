using System;
using Moq;
using GameStore.Domain.Abstructs;
using GameStore.Domain.Concrete;
using GameStore.Domain.Domain;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.HtmlHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using GameStore.WebUI.Models;

namespace GameStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product [] {
                new Product{ProductID = 1, Name = "P1"},
                new Product{ProductID = 2, Name = "P2"},
                new Product{ProductID = 3, Name = "P3"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

           // IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P12");
            Assert.AreEqual(prodArray[1].Name, "P");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pageInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pageInfo, pageUrlDelegate);

            Assert.AreEqual(result.ToString(), @"<a href =""Page1"">1</a>" + 
                @"<a class = ""Selected"" href = ""Page2"" > 2 </a>" + @"<a href = ""Page3"">3</a>");
        }
        [TestMethod]
        public void Can_Send_Pagination_Veiw_Model()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product{ProductID = 1, Name = "Прибулець"},
                    new Product{ProductID = 2, Name = "Ball"},
                    new Product{ProductID = 3, Name = "Knife"},
                    new Product{ProductID = 4, Name = "stick of golf"},
                    new Product{ProductID = 5, Name = "spining coil"}
                }.AsQueryable());

            //Arrange
            ProductController controller = new ProductController(mock.Object);

            //Act
            ProductsListViewModel result = (ProductsListViewModel) controller.List(null, 2).Model;

            //Assert
            PagingInfo paginginfo = result.pagingInfo;
            Assert.AreEqual(paginginfo.CurrentPage, 2);
            Assert.AreEqual(paginginfo.CurrentPage, 3);
            Assert.AreEqual(paginginfo.CurrentPage, 4);
            Assert.AreEqual(paginginfo.CurrentPage, 6);

        }

        [TestMethod]
        public void Cat_Filter_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat3"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat4"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //Action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P1" && result[0].Category == "Cat1");
            Assert.IsTrue(result[1].Name == "P2" && result[1].Category == "Cat2");
        }
        [TestMethod]
        public void Can_Create_Category()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ProductID = 1, Name = "P1", Category = "Apples"},
                new Product{ProductID = 2, Name = "P2", Category = "Apples"},
                new Product{ProductID = 3, Name = "P3", Category = "Chess"},
                new Product{ProductID = 4, Name = "P4", Category = "Chess"},
                new Product{ProductID = 5, Name = "P5", Category = "Plums"},
                new Product{ProductID = 6, Name = "P6", Category = "Plums"}
            }.AsQueryable());

            NavController controller = new NavController(mock.Object);

            //Action
            string[] result = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
        }
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
            new Product{ProductID = 1, Name = "P2", Category = "Appres"},
            new Product{ ProductID = 2, Name = "P3", Category = "Chess"}
            }.AsQueryable());

            NavController controller = new NavController(mock.Object);
            string categoryToSelect = "Appres";

            //Action
            string result = controller.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);
        }
        [TestMethod]
        public void Ganerate_Category_Specific_Product_Count()
        {
           //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
                }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //Action
            int result1 = ((ProductsListViewModel)controller.List("Cat1").Model).pagingInfo.TotalItems;
            int result2 = ((ProductsListViewModel)controller.List("Cat2").Model).pagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(result1, 2);
        }
        
    }
    [TestClass]
    public class CartTest1
    {
        [TestMethod]
    public void Can_Add_New_Line()
        {
            Product pr1 = new Product { ProductID = 1, Name = "P1" };
            Product pr2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(pr1, 1);
            target.AddItem(pr2, 1);
            CartLine[] result = target.Lines.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].productProp, pr1);
        }
    }
}

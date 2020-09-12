using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class ShopBridge_InventoryTest
    {
        [TestMethod]
        public void _ListOfItems()
        {
            // Arrange
            ShopBridge_InventoryController controller = new ShopBridge_InventoryController();

            // Act
            PartialViewResult result = controller._ListOfItems() as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            ShopBridge_InventoryController controller = new ShopBridge_InventoryController();

            // Act
            PartialViewResult result = controller.Create() as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create(Shopbridge_inventoryModel obj)
        {
            obj = new Shopbridge_inventoryModel();
            obj.Name_ = "apple";
            obj.description_ = "Shimla apples";
            obj.price = 100;
            obj.image_name = "apple.png";
            obj.thumb_name ="apple_thumb.jpg";
            // Arrange
            ShopBridge_InventoryController controller = new ShopBridge_InventoryController();

            // Act
            ViewResult result = controller.Create(obj) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

       /* [TestMethod]
        public void DeleteItem()
        {
          string obj = "avocado";
            // Arrange
            ShopBridge_InventoryController controller = new ShopBridge_InventoryController();

            // Act
            PartialViewResult result = controller.DeleteItem(obj) as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }*/

        [TestMethod]
        public void GetDetails()
        {
            string obj = "apple";
            // Arrange
            ShopBridge_InventoryController controller = new ShopBridge_InventoryController();

            // Act
            ViewResult result = controller.GetDetails(obj) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            ShopBridge_InventoryController controller = new ShopBridge_InventoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

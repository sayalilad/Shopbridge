using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using attempt_api.Controllers;
using attempt_api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace attempt_api.Tests.Controllers
{
    [TestClass]
    public class Shopbridge_inventory_1Test
    {
        
        [TestMethod]
        public void GetShopbridge_inventory_1()
        {
            // Arrange
            Shopbridge_inventory_1Controller controller = new Shopbridge_inventory_1Controller();
          
            // Act
            IQueryable<Shopbridge_inventory_1> result = controller.GetShopbridge_inventory_1();

            // Assert
             Assert.IsNotNull(result);
           
        }

        [TestMethod]
        public void GetShopbridge_inventory_1(string id)
        {
            id= "apple";
            // Arrange
            Shopbridge_inventory_1Controller controller = new Shopbridge_inventory_1Controller();

            // Act
            IHttpActionResult result = controller.GetShopbridge_inventory_1(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostShopbridge_inventory_1()
        {
            Shopbridge_inventory_1 obj = new Shopbridge_inventory_1();
            obj.Name_ = "blackGrape";
            obj.description_ = "Nashik special";
            obj.price = 100;
            obj.image_name = "";
            obj.thumb_name = "";

            // Arrange
            Shopbridge_inventory_1Controller controller = new Shopbridge_inventory_1Controller();

            // Act
            IHttpActionResult result = controller.PostShopbridge_inventory_1(obj);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteShopbridge_inventory_1()
        {
            string id = "apple";
            // Arrange
            Shopbridge_inventory_1Controller controller = new Shopbridge_inventory_1Controller();

            // Act
            IHttpActionResult result = controller.DeleteShopbridge_inventory_1(id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteFiles()
        {
            Shopbridge_inventory_1 obj = new Shopbridge_inventory_1();
            obj.Name_ = "blackGrape";
            obj.description_ = "Nashik special";
            obj.price = 100;
            obj.image_name = "";
            obj.thumb_name = "";
            // Arrange
            Shopbridge_inventory_1Controller controller = new Shopbridge_inventory_1Controller();
            // Act
            controller.DeleteFiles(obj);
            // Assert
            if (string.IsNullOrEmpty(obj.thumb_name))
                Assert.IsTrue(true);
            else
            {
                if (File.Exists(HttpContext.Current.Server.MapPath("~/Images/") + obj.image_name) && File.Exists(HttpContext.Current.Server.MapPath("~/Thumbnails/") + obj.thumb_name))
                    Assert.IsTrue(false);
                else
                    Assert.IsTrue(true);

            }
        }
    }
}

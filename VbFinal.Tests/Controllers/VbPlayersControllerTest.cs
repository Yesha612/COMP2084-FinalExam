using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VbFinal.Controllers;
using VbFinal.Models;

// new references
using System.Web.Mvc;

namespace VbFinal.Tests.Controllers
{
    [TestClass]
    public class VbPlayersControllerTest
    {
        VbPlayersController controller;
        Mock<IMockVbPlayer> mock;
        List<VbPlayer> vbPlayers;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IMockVbPlayer>();

            vbPlayers = new List<VbPlayer>
            {
                new VbPlayer
                {
                    VbPlayerId = 961,
                    FirstName = "Lori",
                    Lastname = "S",
                    Photo = "https://img.icons8.com/color/384/beach-volleyball.png"
                },
                new VbPlayer
                {
                    VbPlayerId = 961,
                    FirstName = "Zak",
                    Lastname = "N",
                    Photo = "https://img.icons8.com/ultraviolet/384/beach-volleyball.png"
                }
            };

            mock.Setup(m => m.VbPlayers).Returns(vbPlayers.AsQueryable());
            controller = new VbPlayersController(mock.Object);
        }

        // Index
        [TestMethod]
        public void IndexViewLoads()
        {
            // Arrange
            //Handled in TestInitiliaze

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexValidLoadsVbPlayers()
        {
            // Arrange
            //Handled in TestInitiliaze

            // Act
            var result = (List<VbPlayer>)((ViewResult)controller.Index()).Model;

            // Assert
            CollectionAssert.AreEqual(vbPlayers.ToList(), result);
        }

        // Edit
        [TestMethod]
        public void EditLoadsValidId()
        {
            // Arrange
            //Handled in TestInitiliaze

            // Act
            ViewResult result = controller.Edit(961) as ViewResult;

            // Assert
            Assert.AreEqual("Edit", result.ViewName);

        }

      
        [TestMethod]
        public void EditInvalidId()
        {
            // Arrange
            //Handled in TestInitiliaze

            // Act
            HttpNotFoundResult result = controller.Edit(500) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        

        [TestMethod]
        public void EditSaveValid()
        {
            VbPlayer vbPlayer = new VbPlayer {
                VbPlayerId = 962,
                FirstName = "Laura",
                Lastname = "S",
                Photo = "https://img.icons8.com/color/384/beach-volleyball.png"
            };

            // Arrange 
            // handled in TestInitialize

            // Act
            RedirectToRouteResult result = controller.Edit(vbPlayer) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }
    }
}

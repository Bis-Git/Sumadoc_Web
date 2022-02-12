using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OICAR_Web.Controllers;
using SustavZaLijecnike;

namespace OICAR_Web.Test.Controllers
{
    public class ProfilControllerTest
    {
        private ProfilController controller = new ProfilController();
        [SetUp]
        public void Setup()
        {
            var httpContextStub = new Mock<HttpContext>();
            var sessionStub = new Mock<ISession>();
            var controllerContext = new ControllerContext();
            httpContextStub.Setup(x => x.Session).Returns(sessionStub.Object);
            controllerContext.HttpContext = httpContextStub.Object;
            controllerContext.HttpContext.Session.SetString("userId", "1");
            controller.ControllerContext = controllerContext;
            controller.HttpContext.Session.SetString("userId", "1");
        }

        [Test]
        public void TestProfilView()
        {
            var result = controller.Profil() as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("Profil", result.ViewName);
        }

        
    }
}

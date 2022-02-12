using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OICAR_Web.Controllers;
using SustavZaLijecnike;

namespace OICAR_Web.Test.Controllers
{
    public class PodatciControllerTest
    {
        private PodatciController controller = new PodatciController();
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
        public void TestPovijestBolestiView()
        {
            var result = controller.PovijestBolesti() as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("PovijestBolesti", result.ViewName);
            Assert.IsTrue(model is List<Dijagnoza>);
        }

        [Test]
        public void TestPovijestPregledaView()
        {
            var result = controller.PovijestPregleda() as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("PovijestPregleda", result.ViewName);
            Assert.IsTrue(model is IEnumerable<Pregled>);
        }

        [Test]
        public void TestTerapijeView()
        {
            var result = controller.Terapije() as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("Terapije", result.ViewName);
            Assert.IsTrue(model is IEnumerable<Recept>);
        }

        [Test]
        public void TestTerminiView()
        {
            var result = controller.Termini() as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("Termini", result.ViewName);
            Assert.IsTrue(model is IEnumerable<Termin>);
        }

        [Test]
        public void TestUputniceView()
        {
            var result = controller.Uputnice() as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("Uputnice", result.ViewName);
            Assert.IsTrue(model is IEnumerable<Uputnica>);
        }

        [Test]
        public void TestCijepljenjaView()
        {
            var result = controller.Cijepljenja() as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("Cijepljenja", result.ViewName);
            Assert.IsTrue(model is IEnumerable<Cijepljenje>);
        }

    }
}

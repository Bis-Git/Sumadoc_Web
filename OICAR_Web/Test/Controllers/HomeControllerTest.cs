using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OICAR_Web.Controllers;

namespace OICAR_Web.Test.Controllers
{
    public class HomeControllerTest
    {


        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestLoginView()
        {
            var controller = new HomeController();
            var result = controller.Login() as ViewResult;
            Assert.AreEqual("Login", result.ViewName);
        }

        [Test]
        public void TestUserLogin()
        {
            var controller = new HomeController();
            var result = controller.Login("ipetrov", "ipterov") as ViewResult;
            Assert.AreEqual("Login", result.ViewName);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OICAR_Web.Controllers;

namespace OICAR_Web.Test.Controllers
{
    public class NarudzbeControllerTest
    {

        private NarudzbeController controller = new NarudzbeController();


        [Test]
        public void TestUspijehView()
        {
            var result = controller.Uspijeh("termin") as ViewResult;
            object? model = result.ViewData.Model;
            Assert.AreEqual("Uspijeh", result.ViewName);
        }
    }
}

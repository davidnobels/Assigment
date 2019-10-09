using Xunit;
using Assignment1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace XUnit_1
{
    [TestClass]
    public class SODController_Test : Controller
    {
        [TestMethod]
        public void TestSODView()
        {
            var controller = new SalesOrderDetailsController;
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI_Inflation;
using WebAPI_Inflation.Controllers;
using WebAPI_Inflation.Models;

namespace WebAPI_Inflation.Tests.Controllers
{
    [TestClass]
    public class SerieControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Disponer
            SerieController controller = new SerieController();

            // Actuar
            DataSerie[] result = controller.Get();

            // Declarar
            Assert.IsNotNull(result);            
            Assert.AreNotEqual(0, result.Count());
        }
    }
}

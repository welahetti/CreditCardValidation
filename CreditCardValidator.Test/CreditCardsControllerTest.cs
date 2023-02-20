using Azure;
using CreditCardValidator.API.Controllers;
using CreditCardValidator.Application;
using CreditCardValidator.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Test
{
    [TestFixture]
    public class CreditCardValidatorTests
    {
        [Test]
        public void TestDetailsView()
        {
            ICreditCardValidatorRepository _creditCardValidatorRepository = new CreditCardValidatorRepository();
            ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService(_creditCardValidatorRepository);
            var controller = new CreditCardsController(creditCardValidatorService);
            var result = controller.Post("4111111111111111");
            // Assert
            Assert.AreEqual("http://localhost/api/products/42", result.Value);
        }

    }

}
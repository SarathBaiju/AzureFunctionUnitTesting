using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserFunctionApp.Test.Helper;

namespace UserFunctionApp.Test
{
    //Test class
    [TestClass]
    public class MathOperationTest : FunctionTestHelper
    {
        private  MathOperation _mathOperation;
        private  Mock<IMathFunctions> _mockMathFunction;
       
        [TestInitialize]
        public void InitializeTest()
        {
            _mockMathFunction = new Mock<IMathFunctions>();
            _mathOperation = new MathOperation(_mockMathFunction.Object);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            //Arrange
            var query = new Dictionary<String, StringValues>();
            query.Add("name", "sarath");

            int number1 = 10;
            int number2 = 90;

            var math = new Math
            {
                Number1 = number1,
                Number2 = number2
            };

            var body = JsonConvert.SerializeObject(math);

            _mockMathFunction.Setup(m => m.Sum(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(number1+number2);

            //Act

            var result = await _mathOperation.Run(req: HttpRequestSetup(query, body), log: ILoggerSetup());

            var resultObject = result as OkObjectResult;

            //Assert
            Assert.AreEqual(resultObject.Value, number1+number2);
        }
    }
}

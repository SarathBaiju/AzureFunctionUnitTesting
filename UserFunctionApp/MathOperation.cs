using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace UserFunctionApp
{
    public class MathOperation
    {
        private readonly IMathFunctions _mathFunctions;

        public MathOperation(IMathFunctions mathFunctions)
        {
            this._mathFunctions = mathFunctions;
        }

        [FunctionName("add")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Math>(requestBody);

            var result = await _mathFunctions.Sum(data.Number1, data.Number2);

            return new OkObjectResult(result);
        }
    }

    public class Math
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }

    public interface IMathFunctions
    {
        Task<int> Sum(int number1, int number2);
    }
    public class MathFunctions : IMathFunctions
    {
        public async Task<int> Sum(int number1, int number2)
        {
            return number1 + number2;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Managers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITestManager _testManager;

        public TestController(ILogger<TestController> logger, ITestManager testManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _testManager = testManager ?? throw new ArgumentNullException(nameof(testManager));
        }

        [HttpGet]
        public async Task Get() => await _testManager.DoSomething();

        [HttpPost]
        public async Task Post([FromBody] TestControllerPostRequest request) => await _testManager.WriteNameToQueue(request.Name);
    }
}

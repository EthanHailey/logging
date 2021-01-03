using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Managers
{
    public class TestManager : ITestManager
    {
        private readonly ILogger<TestManager> _logger;
        private readonly ITestRepository _testRepository;
        private readonly IQueueService _queueService;

        public TestManager(ILogger<TestManager> logger, ITestRepository testRepository, IQueueService queueService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
            _queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));
        }

        public async Task<List<string>> DoSomething()
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(25));
                var strings = await _testRepository.GetStrings();

                // 1/10 chance to throw a fake exception
                if (new Random().Next(0, 10) == 9)
                {
                    throw new Exception("You done went and broke it...");
                }

                return strings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed when doing something");
                throw;
            }
        }

        public async Task WriteNameToQueue(string name)
        {
            _queueService.Enqueue(name);
            await Task.CompletedTask;
        }
    }
}

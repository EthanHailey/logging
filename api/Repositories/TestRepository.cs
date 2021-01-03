using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace api.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ILogger<TestRepository> _logger;
        public TestRepository(ILogger<TestRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<string>> GetStrings()
        {
            var timer = new Stopwatch();

            // Mock returning from a database
            // Eventually actually add a database
            var items = new List<string>
            {
                "Item0",
                "Item1",
                "Item2",
                "Item3",
                "Item4",
            };

            timer.Start();
            // Simulate fetching items from the database
            await Task.Delay(new Random().Next(5, 150));
            timer.Stop();

            _logger.LogInformation("Fetched strings from database in {ElapsedMilliseconds}ms", timer.ElapsedMilliseconds);
            
            return items;
        }
    }
}
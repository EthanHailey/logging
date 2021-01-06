using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.RabbitMQ;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace func
{
    public static class func
    {
        // This is a duplicate of the class in api.Models
        // and should be broken out into some shared library, but I'm
        // lazy and this is just a demo.
        public class QueueMessage
        {
            public string TraceId { get; set; }
            public string Name { get; set; }
        }

        [FunctionName("func")]
        public static async Task Run([RabbitMQTrigger("demo-queue", ConnectionStringSetting = "RABBITMQ_CONNECTION_STRING")] QueueMessage payload, ILogger logger)
        {
            logger.LogInformation("Processing queue message from {User} with traceId {TraceId}...", payload.Name, payload.TraceId);

            var timer = new Stopwatch();
            try
            {
                timer.Start();

                await Task.Delay(TimeSpan.FromMilliseconds(new Random().Next(150, 750)));

                // 1/10 chance to throw a fake exception
                if (new Random().Next(0, 10) == 9)
                {
                    throw new Exception("Failed to process queue message");
                }
                timer.Stop();
                logger.LogInformation("Completed processing queue message with traceId {TraceId} in {ElapsedMilliseconds}ms.", payload.TraceId, timer.ElapsedMilliseconds, payload.TraceId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process queue message with traceId {TraceId}", payload.TraceId);
                throw ex;
            }
        }
    }
}
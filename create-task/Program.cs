using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var redisConnectionString = builder.Build().GetConnectionString("Redis");

GlobalConfiguration.Configuration
    .UseColouredConsoleLogProvider()
    .UseRedisStorage(redisConnectionString);

// fire-and-forget
var fireAndForgetJobName = "test job: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
Console.WriteLine($"create a background job:{fireAndForgetJobName}");
BackgroundJob.Enqueue(() => Console.WriteLine(fireAndForgetJobName));

// delayed
var delayedJobName = "delayed job: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
Console.WriteLine($"create a delayed job: {delayedJobName}");
BackgroundJob.Schedule(() => Console.WriteLine(delayedJobName), TimeSpan.FromSeconds(5));

// recurring
var recurringJobName = "recurring job: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
var recurringJobMessage = $"create a recurring job:{recurringJobName}";
Console.WriteLine(recurringJobMessage);
RecurringJob.AddOrUpdate("MyJobId", () => Console.Write(recurringJobName), "0,15,30,45 * * * *");

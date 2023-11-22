using System.Linq.Expressions;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Configuration;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true);
var redisConnectionString = builder.Build().GetConnectionString("Redis");
Console.WriteLine("Redis ConnectionString: " + redisConnectionString);
GlobalConfiguration.Configuration
    .UseColouredConsoleLogProvider()
    .UseRedisStorage(redisConnectionString);

// fire-and-forget
Expression<Action> expression = () => Console.WriteLine("test job: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
BackgroundJob.Enqueue(expression);

// delayed
var delayedJobName = "delayed job: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
BackgroundJob.Schedule(() => Console.WriteLine(delayedJobName), TimeSpan.FromSeconds(5));

// recurring
var recurringJobName = "recurring job: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
RecurringJob.AddOrUpdate("MyJobId", () => Console.Write(recurringJobName), "0,15,30,45 * * * *");

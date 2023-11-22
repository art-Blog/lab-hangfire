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


using var server = new BackgroundJobServer();
Console.WriteLine("Hangfire Server started. Press any key to exit...");
while (true)
{
    Thread.Sleep(Timeout.Infinite);
}
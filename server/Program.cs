using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var redisConnectionString = builder.Build().GetConnectionString("Redis");
Console.WriteLine("Redis ConnectionString: " + redisConnectionString);


GlobalConfiguration.Configuration
    .UseColouredConsoleLogProvider()
    .UseRedisStorage(redisConnectionString);


using var server = new BackgroundJobServer();
Console.WriteLine("Hangfire Server started. Press any key to exit...");
Console.ReadKey();
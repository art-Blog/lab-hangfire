using dashboard.Model;
using Hangfire;
using Hangfire.Redis.StackExchange;

var builder = WebApplication.CreateBuilder(args);
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHangfire(config =>
{
    config.UseRedisStorage(redisConnectionString);
    config.UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.ServerCount) //服务器数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.RecurringJobCount) //任务数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.RetriesCount) //重试次数
        //.UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.EnqueuedCountOrNull)//队列数量
        //.UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.FailedCountOrNull)//失败数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.EnqueuedAndQueueCount) //队列数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.ScheduledCount) //计划任务数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.ProcessingCount) //执行中的任务数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.SucceededCount) //成功作业数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.FailedCount) //失败数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.DeletedCount) //删除数量
        .UseDashboardMetric(Hangfire.Dashboard.DashboardMetrics.AwaitingCount); //等待任务数量
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHangfireDashboard(
    pathMatch: "/hangfire",
    options: new DashboardOptions()
    {
        // 進入 hangfire dashboard 的授權規則 (有沒有權限看 dashboard 就看這個邏輯怎麼設定)
        Authorization = new[] { new MyAuthFilter() }
    });

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/hangfire");
    return Task.CompletedTask;
});
app.MapRazorPages();

app.Run();
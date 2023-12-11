using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Schedule;

namespace website.Controllers;

public class JobController : Controller
{
    private readonly IMyHelloWorld _myHelloWorld;
    private readonly IMyRecurringJob _myRecurringJob;
    private readonly IMyDelayJob _myDelayJob;

    public JobController(IMyHelloWorld myHelloWorld, IMyDelayJob myDelayJob, IMyRecurringJob myRecurringJob)
    {
        _myHelloWorld = myHelloWorld;
        _myDelayJob = myDelayJob;
        _myRecurringJob = myRecurringJob;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public JsonResult AddFireAndForgetJob()
    {
        _myHelloWorld.Run();
        BackgroundJob.Enqueue(() => _myHelloWorld.Run());
        return Json(new { isSuccess = true, jobType = "FireAndForget" });
    }

    [HttpPost]
    public JsonResult AddDelayedJob()
    {
        var delaySeconds = TimeSpan.FromSeconds(10);

        _myDelayJob.Run();
        BackgroundJob.Schedule(() => _myDelayJob.Run(), delaySeconds);
        return Json(new { isSuccess = true, jobType = "Delayed" });
    }

    [HttpPost]
    public JsonResult AddRecurringJob()
    {
        const string cronExpression = "0,15,30,45 * * * *";
        var jobId = Guid.NewGuid().ToString();

        _myRecurringJob.Run();
        RecurringJob.AddOrUpdate(jobId, () => _myRecurringJob.Run(), cronExpression);
        return Json(new { isSuccess = true, jobType = "Recurring" });
    }
}
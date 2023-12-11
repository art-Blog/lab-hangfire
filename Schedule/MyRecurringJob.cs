using Schedule.Base;

namespace Schedule;

public class MyRecurringJob : IMyRecurringJob
{
    public void Run()
    {
        Console.WriteLine("Hello, world from recurring job!");
    }
}

public interface IMyRecurringJob : IScheduleBase
{
}
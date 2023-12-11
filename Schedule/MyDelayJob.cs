using Schedule.Base;

namespace Schedule;

public class MyDelayJob : IMyDelayJob
{
    public void Run()
    {
        Console.WriteLine("Hello, world from delay job!");
    }
}

public interface IMyDelayJob : IScheduleBase
{
}
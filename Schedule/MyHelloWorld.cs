using Schedule.Base;

namespace Schedule;

public class MyMyHelloWorld : IMyHelloWorld
{
    public void Run()
    {
        Console.WriteLine("Hello, world from MyHelloWorld!");
    }
}

public interface IMyHelloWorld : IScheduleBase
{
}
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection()
    .AddTransient<ILogService, GreenLogSerivce>()
    .AddTransient<Logger>();

var provider = services.BuildServiceProvider();



var logger1 = new Logger(provider.GetRequiredService<ILogService>()); //сервис или исключение
logger1.Log(" Hello, Kim");

logger1 = provider.GetRequiredService<Logger>();
logger1.Log(" Hello, Kim");

interface ILogService
{
    void Write(string message);
}

class SimpleLogSerivce : ILogService
{

    public void Write(string message) => Console.WriteLine(message);
    
}

class GreenLogSerivce : ILogService
{

    public void Write(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }

}

class Logger
{
    ILogService service;

    public ILogService Service {
        get { return service; }
        set { service = value; }
    }

    public Logger(ILogService service)
    {
        this.service = service;
    }

    public void Log(string message) => service.Write($"{DateTime.Now}{message}");
}
using System.Net.Sockets;
using System.Text;

try
{
    using TcpClient tcpClient = new();
    Console.WriteLine("Клиент запущен");

    await tcpClient.ConnectAsync("127.0.0.1", 1234);
    Console.WriteLine("Подключение установлено");

    var stream = tcpClient.GetStream();

    while (tcpClient.Connected)
    {
        string? message = Console.ReadLine();

        if (message == "exit")
            break;

        byte[] data = Encoding.UTF8.GetBytes($"{message}");
        try
        {
            await stream.WriteAsync(data);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"ОШИБКА: {ex.Message}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ОШИБКА: {ex.Message}");
}
finally
{
    Console.WriteLine("Клиент отключён");
}
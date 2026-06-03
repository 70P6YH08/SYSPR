using System.Net.Sockets;
using System.Text;

try
{
    using TcpClient tcpClient = new();
    Console.WriteLine("Клиент запущен");

    await tcpClient.ConnectAsync("127.0.0.1", 1234);
    Console.WriteLine("Подключение установлено");

    byte[] data = new byte[512];
    var stream = tcpClient.GetStream();

    while (tcpClient.Connected)
    {
        int bytes = await stream.ReadAsync(data);
        if (bytes == 0)
            break;

        string message = Encoding.UTF8.GetString(data, 0, bytes);
        Console.WriteLine(message);

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
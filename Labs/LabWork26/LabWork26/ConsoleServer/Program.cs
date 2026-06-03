using System.Net;
using System.Net.Sockets;
using System.Text;

IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
TcpListener tcpListener = new(iPAddress, 1234);

try
{
    tcpListener.Start();
    Console.WriteLine("Сервер запущен");

    while (true)
    {
        var tcpClient = await tcpListener.AcceptTcpClientAsync();
        Task.Run(async () => await ProcessClientConnect(tcpClient));
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ОШИБКА: {ex.Message}");
}
finally
{
    Console.WriteLine("Сервер отключён");
}

async Task ProcessClientConnect(TcpClient tcpClient)
{

    Console.WriteLine($"Подключился: {tcpClient.Client.RemoteEndPoint}");

    using (tcpClient)
    {
        var stream = tcpClient.GetStream();
        byte[] data = new byte[512];

        while (true)
        {
            int bytes = await stream.ReadAsync(data);

            if (bytes == 0)
                break;

            string message = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} ({DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}): {message}");
        }
    }
}
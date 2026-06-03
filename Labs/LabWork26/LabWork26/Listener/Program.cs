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
        using (TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync())
        {
            Console.WriteLine($"Подключился: {tcpClient.Client.RemoteEndPoint}");
            var stream = tcpClient.GetStream();

            while (tcpClient.Connected)
            {
                byte[] data = Encoding.UTF8.GetBytes($"({DateTime.Now}) привет");
                stream.WriteAsync(data);
                await Task.Delay(5000);
            }
        }
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
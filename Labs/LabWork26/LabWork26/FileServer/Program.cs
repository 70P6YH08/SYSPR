using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
    string clientId = tcpClient.Client.RemoteEndPoint.ToString();

    Console.WriteLine($"Подключился: {clientId}");

    using (tcpClient)
    {
        var stream = tcpClient.GetStream();

        byte[] weightSize = new byte[8];
        byte[] fileNameSize = new byte[512];
        byte[] imageSize = new byte[8192];

        while (tcpClient.Connected)
        {
            int countBytes = 8;
            int bytes = await stream.ReadAsync(fileNameSize);

            string message = Encoding.UTF8.GetString(fileNameSize, 0, bytes);
            Console.WriteLine($"{clientId} ({DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}): {message}");

            await stream.ReadAsync(weightSize, 0, countBytes);
            long fileWight = BitConverter.ToInt64(weightSize, 0);
            Console.WriteLine($"Размер изначального файла: {fileWight}");

            byte[] imageBytes = new byte[fileWight];
            int bytesRead = 0;

            while (bytesRead < fileWight)
            {
                bytesRead += await stream.ReadAsync(imageBytes, bytesRead, (int)(fileWight - bytesRead));
            }

            using var originalImage = new Bitmap(new MemoryStream(imageBytes));

            int newWidth = originalImage.Width / 2;
            int newHeight = originalImage.Height / 2;

            using var resizedImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            using MemoryStream outputStream = new();
            resizedImage.Save(outputStream, ImageFormat.Jpeg);
            byte[] resizedBytes = outputStream.ToArray();
            Console.WriteLine($"Размер изменённого файла: {resizedBytes.Length}");

            await stream.WriteAsync(BitConverter.GetBytes(resizedBytes.Length), 0, countBytes / 2);
            await stream.WriteAsync(resizedBytes, 0, resizedBytes.Length);
        }
    }
}
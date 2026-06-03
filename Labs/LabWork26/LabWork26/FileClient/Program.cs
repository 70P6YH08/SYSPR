using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Text;

try
{
    using TcpClient tcpClient = new();
    Console.WriteLine("Клиент запущен");

    await tcpClient.ConnectAsync("127.0.0.1", 1234);
    Console.WriteLine("Подключение установлено");

    var stream = tcpClient.GetStream();

    int countBytes = 8;

    byte[] sizeBuffer = new byte[8];
    byte[] imageBuffer = new byte[8192];

    string pictureFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    DirectoryInfo directoryInfo = new(Path.Combine(pictureFolderPath, "resized_pictures"));
    if (!directoryInfo.Exists)
        Directory.CreateDirectory(directoryInfo.FullName);

    while (tcpClient.Connected)
    {
        Console.Write("Введите имя файла: ");
        string? fileNamePath = Console.ReadLine();

        if (!File.Exists(fileNamePath))
        {
            Console.WriteLine("Файл не найден");
            continue;
        }

        if (fileNamePath == "exit")
            break;

        byte[] fileNameBytes = Encoding.UTF8.GetBytes($"{fileNamePath}");
        try
        {
            await stream.WriteAsync(fileNameBytes);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"ОШИБКА: {ex.Message}");
        }

        FileInfo fileInfo = new(fileNamePath);

        byte[] sizeBytes = BitConverter.GetBytes(fileInfo.Length);
        await stream.WriteAsync(sizeBytes, 0, countBytes);
        Console.WriteLine($"Размер изначального файла: {fileInfo.Length}");

        byte[] imageBytes = File.ReadAllBytes(fileInfo.FullName);
        await stream.WriteAsync(imageBytes, 0, imageBytes.Length);

        await stream.ReadAsync(sizeBuffer, 0, countBytes / 2);
        long resizedFileWeight = BitConverter.ToInt64(sizeBuffer, 0);
        Console.WriteLine($"Размер изменённого файла: {resizedFileWeight}");

        int bytesRead = 0;

        byte[] resizedImageBytes = new byte[resizedFileWeight];

        while (bytesRead < resizedFileWeight)
        {
            bytesRead += await stream.ReadAsync(resizedImageBytes, bytesRead, (int)(resizedFileWeight - bytesRead));
        }

        using (MemoryStream memoryStream = new(resizedImageBytes))
        {
            using (Bitmap bitmap = new(memoryStream))
            {
                bitmap.Save(Path.Combine(directoryInfo.FullName, $"resized_{fileInfo.Name}"), ImageFormat.Jpeg);
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
    Console.WriteLine("Клиент отключён");
}
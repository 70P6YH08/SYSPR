CancellationTokenSource cancellationTokenSource = new();
CancellationToken cancellationToken = cancellationTokenSource.Token;

Task downloadFile = new(() =>
{
    DownloadFiles();
}, cancellationToken);

void DownloadFiles()
{
    int maxDivision = 100;
    int sizeProgressString = 20;
    int percent = 0;
    Random random = new();

    while (percent < maxDivision)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"\nОтмена операции на {percent}%");
            return;
        }

        int inc = random.Next(1, 10);
        percent = Math.Min(percent + inc, maxDivision);
        int filled = percent * sizeProgressString / maxDivision;

        string progressString = new string('=', filled) + new string('-', sizeProgressString - filled);
        Console.Write($"\r[{progressString}] {percent}%");

        Thread.Sleep(1000);
    }
    Console.Write("\nЗагрузка завершена!");
}

downloadFile.Start();

var cancelTask = Console.ReadKey().Key;
if (cancelTask == ConsoleKey.C)
{
    cancellationTokenSource.Cancel();
    downloadFile.Wait();
    Console.WriteLine("Задача завершена");
}

cancellationTokenSource.Dispose();

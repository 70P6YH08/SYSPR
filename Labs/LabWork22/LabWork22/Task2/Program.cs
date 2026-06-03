string commonVar = "";
object locker = new();

Thread myThread = new(MyThreadMethod);
myThread.Start();

var close = Console.ReadKey().Key;

while (true)
{
    if (close == ConsoleKey.X)
    {
        lock (locker)
            commonVar = "x";
        break;
    }
}

myThread.Join(); //для корректного завершения программы, чтоб main завершался после потока mythread
//lock для блокировки блока кода до завершения исполняемого блока кода для синхронизации потоков. Остальные потоки блокируются в lock-e с объектом locker
void MyThreadMethod()
{
    string currentVar;
    while (true)
    {
        lock (locker)
            currentVar = commonVar;

        if (currentVar == "x")
            break;
        Console.WriteLine("Пока работает");
        Thread.Sleep(800);
    }
}

int counter = 0;
object locker = new();

for (int i = 0; i < 5; i++)
{
    Thread newThread = new(CounterIncrement);
    var threadId = newThread.ManagedThreadId;
    newThread.Start(threadId);
}

void CounterIncrement(object? threadId)
{
    for (int i = 0; i < 1000; i++)
    {
        lock (locker)
        {
            counter++;
            Console.WriteLine($"{threadId}: {counter}");
        }

        Thread.Sleep(1);
    }
}
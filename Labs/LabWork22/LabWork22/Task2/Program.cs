string commonVar = "a";

Thread myThread = new(MyThreadMethod);

myThread.Start(commonVar);

Thread.Sleep(4000);
commonVar = "x";

void MyThreadMethod(object? obj)
{
    if (obj is string str)
    {
        while (str != "x")
        {
            Console.WriteLine("Пока работает");
            Thread.Sleep(800);
            str = commonVar;
        }
    }
}

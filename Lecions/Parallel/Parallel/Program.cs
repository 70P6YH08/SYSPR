using System.Threading;
using System.Threading.Tasks;

//Thread currentThread = Thread.CurrentThread;

//Console.WriteLine(currentThread.ManagedThreadId);

//Thread.Sleep(500);

//Thread printThread1 = new(() =>
//{
//    while(true)
//    {
//        Console.WriteLine("1");
//        Thread.Sleep(1000);
//    }
//});

//Thread printThread2 = new(() =>
//{
//    while(true)
//    {
//        Console.WriteLine("2");
//        Thread.Sleep(1000);
//    }
//});

//printThread1.Start();
//printThread2.Start();


Task task1 = new Task(() => Console.WriteLine("1"));
task1.Start();

Task task2 = Task.Factory.StartNew(() => Console.WriteLine("2"));

Task task3 = Task.Run(() => Console.WriteLine("3"));


task1.Wait();

var tasks = new Task[5];
for (int i = 0; i < 5; i++)
{
    tasks[i] = Task.Run(() => Console.WriteLine(i));
}

Task.WaitAll(tasks);

Task<int> taskRes = new Task<int>(() =>
{
    Thread.Sleep(1000);
    return 10;
});

taskRes.Start();

Console.WriteLine(taskRes.Result);
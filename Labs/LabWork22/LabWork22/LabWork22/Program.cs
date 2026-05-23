internal class Program
{
    private static void Main(string[] args)
    {
        Thread newthread = new(Print);
        newthread.Start();

        void Print()
        {
            while (true)
            {
                Console.WriteLine(1);
                Thread.Sleep(1000);
            }
        }
        while (true)
        {
            Console.WriteLine(0);
            Thread.Sleep(1500);
        }
    }
}
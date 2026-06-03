int incValue = 0;
for (int i = 0; i < 10; i++)
{
    Interlocked.Increment(ref incValue);
    Console.WriteLine(incValue);
}
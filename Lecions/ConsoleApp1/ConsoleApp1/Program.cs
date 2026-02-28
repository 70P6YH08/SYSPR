
Person person = new Person() { name = "Roman", age = 23 };

ChangePerson(ref person);

Test();

Console.WriteLine(person.name);
Console.WriteLine(person.age);

Console.WriteLine(GC.GetGeneration(person));
Console.WriteLine(GC.GetTotalMemory(false));
GC.Collect();
GC.WaitForPendingFinalizers();
Console.WriteLine(GC.GetTotalMemory(false));
Console.WriteLine(GC.GetGeneration(person));

GC.AddMemoryPressure(100000);
GC.RemoveMemoryPressure(100000);

unsafe
{
    int* x;
    int y = 10;

    x = &y;
    Console.WriteLine(*x);
    y = y + 10;
    Console.WriteLine(*x);
}

void Test()
{
    using Person person1 = new Person() { name = "RIP" };


    try
    {
        Person person2 = new Person();
    }
    finally
    {
        person.Dispose();
    }
}

void ChangePerson(ref Person person)
{
    person.name = "NoName";
    person = new Person() { name = "Bill", age = 123 };
    Console.WriteLine(person.name);
}

class Person : IDisposable
{
    public string name = "";
    public int age;

    //~Person()
    //{
    //    Console.WriteLine($"{name} deleted");
    //}

    public void Dispose()
    {
        Console.WriteLine($"{name} deleted");
        GC.SuppressFinalize(this);
    }

    //protected override void Finalize()
    //{
    //    try
    //    {
    //        //тут инструкция деструктора
    //    }
    //    finally
    //    {
    //        base.Finalize();
    //    }
    //}
}
using System.Text;

class Programm
{
    static Test globalRef;

    static byte[] data1 = new byte[70000];
    static byte[] data2 = new byte[90000];
    static byte[] data3 = new byte[100000];

    byte[] packet =
{
0x01, 0x00, // ushort ID = 1
0x10, 0x27, 0x00, 0x00, // int timestamp = 10000
0x00, 0x00, 0x48, 0x42, // float temperature = 50.0
0x01, // byte status
0x00, 0x00, 0x00, 0x00 // int checksum
};

    static void Main()
    {

        //Task 1

        CreateObject();
        GC.Collect();
        //int generation = GC.GetGeneration(globalRef);
        //Console.WriteLine($"Поколение объекта: {generation}");

        void CreateObject()
        {
            Test t = new Test();
            globalRef = t;
        }

        Console.WriteLine();

        //Task 2

        PointClass pointClass = new PointClass() { X = 10, Y = 15 };
        PointStruct pointStruct = new PointStruct() { X = 20, Y = 25 };

        Console.WriteLine($"Исходные значения класса: X = {pointClass.X} Y = {pointClass.Y}");
        Console.WriteLine($"Исходные значения структуры: X = {pointStruct.X} Y = {pointStruct.Y}");

        ModifyStruct(ref pointStruct);
        ModifyClass(pointClass);

        Console.WriteLine();

        Console.WriteLine($"Изменённые значения класса: X = {pointClass.X} Y = {pointClass.Y}");
        Console.WriteLine($"Изменённые значения структуры: X = {pointStruct.X} Y = {pointStruct.Y}");

        Console.WriteLine();

        //Task 3

        int genData1 = GC.GetGeneration(data1);
        int genData2 = GC.GetGeneration(data2);
        int genData3 = GC.GetGeneration(data3);

        Console.WriteLine($"Поколение 1 объекта {genData1}" +
            $"\nПоколение 2 объекта {genData2}" +
            $"\nПоколение 3 объекта {genData3}");

        Console.WriteLine();

        //Task 4

        FileLogger fileLogger2 = new FileLogger();
        fileLogger2.LogWriter();
        fileLogger2.Dispose();

        FileLogger fileLogger1 = new FileLogger();
        fileLogger1.LogWriter();



    }

    static void ModifyClass(PointClass pointClass)
    {
        pointClass.X = 50;
        pointClass.Y = 60;
    }

    static void ModifyStruct(ref PointStruct pointStruct)
    {
        pointStruct.X = 40;
        pointStruct.Y = 30;
    }

}

class Test
{
    ~Test()
    {
        Console.WriteLine("Object finalized");
    }
}

class PointClass
{
    public int X;
    public int Y;

}

struct PointStruct
{
    public int X;
    public int Y;


}

class FileLogger : IDisposable
{
    public StreamWriter streamWriter = new StreamWriter("log.txt", true, Encoding.UTF8);

    public void LogWriter()
    {
        streamWriter.WriteLine($"{DateTime.Now}\n");
    }

    public void Dispose()
    {
        streamWriter.Dispose();
    }
}

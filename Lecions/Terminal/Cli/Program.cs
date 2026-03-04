//foreach (var arg in args)
//{
//    Console.WriteLine(arg);
//}

//if (File.Exists(args[0]))
//{
//    Console.WriteLine(File.ReadAllText(args[0]));
//}

//Console.ReadLine();

using System.CommandLine;
using System.ComponentModel;

RootCommand root = new RootCommand("Приложение CLI");

//string, string[], int, bool, FileInfo, enums
Option<FileInfo> fileOption = new Option<FileInfo>("--file", "-f", "-F")
{
    Description = "Файл вывода на консоль",
    Required = true,
};

Option<ConsoleColor> colorOption = new Option<ConsoleColor>("--color", "-c")
{
    Description = "Цвет текста в консоли",
    DefaultValueFactory = result => ConsoleColor.White //значение по умолчанию для опции
};

Command readFileCommand = new Command("read", "Чтение файла")
{
    fileOption,
    colorOption,
};

root.Subcommands.Add(readFileCommand);

readFileCommand.SetAction(res =>
{
    PrintFile(
        res.GetValue(fileOption),
        res.GetValue(colorOption));
});

//root.Options.Add(fileOption);

var result = root.Parse(args);

result.Invoke(); // выполнение действия

//if (result.GetValue(fileOption) is FileInfo file)
//{
//    PrintFile(file);
//}



void PrintFile(FileInfo file, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(File.ReadAllText(file.FullName));
    Console.ForegroundColor = ConsoleColor.White;
}
using System.CommandLine;

RootCommand root = new RootCommand("Приложение CLI");

Option<DirectoryInfo> directoryOption = new Option<DirectoryInfo>("--file", "-f", "-F")
{
    Description = "Вывод файлов на консоль",
    Required = true,
};

Option<string> sort = new Option<string>("--sortType", "-s", "-S", "-desc", "-DESC")
{
    Description = "Сортировка",
    Required = true,
};


Command readCommand = new Command("read", "- Просмотр директории")
{
    directoryOption,
    sort
};

root.Subcommands.Add(readCommand);

readCommand.SetAction(res =>
{
    DirInfo(
        res.GetValue(directoryOption),

        res.GetValue(sort)
    );
});


var result = root.Parse(args);

result.Invoke();


void DirInfo(DirectoryInfo directory, string sortType)
{
    var list = new List<FileInfo>();


    foreach (var file in directory.GetFiles())
    {
        list.Add(file);
    }

    switch (sortType)
    {
        case "-s":
            list.OrderBy(x => x.CreationTime);
            break;
        case "-desc":
            list.OrderByDescending(x => x.CreationTime);
            break;
        default:
            list.OrderBy(x => x.Name);
            break;
    }

    foreach (var file in list)
    {
        Console.WriteLine($"{file
            .CreationTime.ToString("/yyyy/MM/dd/")
            .Replace(".", "/")}{file.Name}");
    }
}
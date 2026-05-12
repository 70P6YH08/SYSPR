using System.CommandLine;

RootCommand root = new RootCommand("Приложение CLI");

Option<DirectoryInfo> directoryOption = new Option<DirectoryInfo>("--file", "-f", "-F")
{
    Description = "Вывод файлов на консоль",
    Required = true,
};

Option<string> sort = new Option<string>("--sortType", "-s", "-S")
{
    Description = "Сортировка",
    Required = false,
};


Command readCommand = new Command("read", "- Просмотр директории")
{
    directoryOption, sort
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


static void DirInfo(DirectoryInfo directory, string sortType)
{
    var list = new List<FileInfo>();

    switch (sortType)
    {
        case "date":
            GetFileSortByDate(directory);
            break;
        case "ext":
            GetFileSortByExtension(directory);
            break;
        case "move":
            MoveToRoot(directory, list);
            break;
        default:
            Console.WriteLine("Неизвестный тип сортировки");
            break;
    }
}

static void GetFileSortByDate(DirectoryInfo directory)
{
    var list = directory.GetFiles();
    var sortedList = list.OrderBy(x => x.CreationTime).ToList();
    foreach (var file in sortedList)
        Console.WriteLine($"{file.CreationTimeUtc.ToString("/yyyy/MM/dd/").Replace(".", "/")}{file.Name}");
}

static void GetFileSortByExtension(DirectoryInfo directory)
{
    var list = directory.GetFiles();
    var sortedList = list.OrderBy(x => x.Extension).ToList();
    foreach (var file in sortedList)
        Console.WriteLine($"{file.Extension.ToUpper().Replace(".", "/")}/{file.Name}");
}



static void MoveToRoot(DirectoryInfo directory, List<FileInfo> list)
{
    var allFiles = GetAllFiles(directory, list);

    foreach (var file in allFiles)
    {
        string targetPath = Path.Combine(directory.FullName, file.Name);
        int counter = 1;

        while (File.Exists(targetPath))
        {
            string newName = Path.GetFileNameWithoutExtension(file.Name) + $" ({counter++})" + file.Extension;
            targetPath = Path.Combine(directory.FullName, newName);
        }

        file.MoveTo(targetPath);
        Console.WriteLine($"Перемещён: {file.FullName} -> {targetPath}");
    }

    RemoveEmptyDirectories(directory);
}

static List<FileInfo> GetAllFiles(DirectoryInfo directory, List<FileInfo> list)
{
    foreach (var file in directory.GetFiles())
        list.Add(file);

    foreach (var dir in directory.GetDirectories())
        GetAllFiles(dir, list);
    return list;
}

static void RemoveEmptyDirectories(DirectoryInfo dir)
{
    try
    {
        foreach (var subDir in dir.GetDirectories())
        {
            RemoveEmptyDirectories(subDir);
            if (!subDir.GetFiles().Any() && !subDir.GetDirectories().Any())
            {
                subDir.Delete();
                Console.WriteLine($"Удалена пустая папка: {subDir.FullName}");
            }
        }
    }
    catch (UnauthorizedAccessException) { }
}


int i = 0;
int dirs = 10;
int floors = 3;
int files = 8;

DirectoryInfo directoryInfo = new(@"C:\temp\NestedFolders\asdf");
string dirPath = directoryInfo.FullName;

NewDirs(dirPath);

void NewDirs(string dir)
{
    while (i <= floors)
    {
        var newSubDirPath = Path.Combine(dir, $"new_sub_dir{i}");
        Directory.CreateDirectory(newSubDirPath);
        for (int j = 0; j < dirs; j++)
        {
            var newDirPath = Path.Combine(newSubDirPath, $"new_dir{j}");
            Directory.CreateDirectory(newDirPath);
            for (int k = 0; k <= files; k++)
            {
                var newFilePath = Path.Combine(newDirPath, $"new_file{k}.txt");
                var createdFile = File.Create(newFilePath);
                createdFile.Close();
                string fileName = createdFile.Name;
                if (k % 3 == 0)
                {
                    using (StreamWriter fileWriter = new StreamWriter(fileName, true))
                        fileWriter.WriteLine($"Это {k} файл в {j} папке {i} подпапке");
                }

            }
        }
        i++;
        NewDirs(newSubDirPath);
    }
}
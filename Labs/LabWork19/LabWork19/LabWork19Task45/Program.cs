using System.CommandLine;
using System.Security.Cryptography;

RootCommand root = new RootCommand("Task 4-5");

Option<DirectoryInfo> directoryOption = new Option<DirectoryInfo>("--file", "-f", "-F")
{
    Description = "Вывод файлов на консоль",
    Required = true,
};

Option<bool> search = new Option<bool>("--search", "-s", "-S")
{
    Description = "Поиск во всех каталогах",
    Required = false,
};

Option<bool> deleteDup = new Option<bool>("--search", "-d", "-D")
{
    Description = "Удаление дубликатов",
    Required = false,
};

Command readCommand = new Command("read", "- Просмотр директории")
{
    directoryOption, search, deleteDup
};

root.Subcommands.Add(readCommand);

readCommand.SetAction(res =>
{
    DirInfo(
        res.GetValue(directoryOption),
        res.GetValue(search),
        res.GetValue(deleteDup)
    );
});


var result = root.Parse(args);

result.Invoke();

static void DirInfo(DirectoryInfo directory, bool searchType, bool deleteDup)
{
    var list = new List<FileInfo>();

    if (searchType)
        GetAllFiles(directory, list);
    else
        GetFile(directory, list);

    var hashFileList = new List<(string Hash, FileInfo File)>();
    foreach (var file in list)
    {
        string hash = ComputeHash(file);
        if (hash != null)
            hashFileList.Add((hash, file));
    }

    hashFileList.Sort((a, b) => a.Hash.CompareTo(b.Hash));

    int i = 0;
    while (i < hashFileList.Count)
    {
        int j = i;
        while (j < hashFileList.Count && hashFileList[j].Hash == hashFileList[i].Hash)
            j++;

        if (j - i > 1)
        {
            Console.WriteLine($"\nДубликаты (хэш {hashFileList[i].Hash}):");
            for (int k = i; k < j; k++)
            {
                Console.WriteLine(hashFileList[k].File.FullName);
                if (deleteDup && k > i)
                {
                    try
                    {
                        hashFileList[k].File.Delete();
                        Console.WriteLine($"Удалён: {hashFileList[k].File.FullName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
            }
        }
        i = j;
    }
}

static void GetFile(DirectoryInfo directory, List<FileInfo> list)
{
    foreach (var file in directory.GetFiles())
        list.Add(file);
}


static void GetAllFiles(DirectoryInfo directory, List<FileInfo> list)
{
    foreach (var file in directory.GetFiles())
        list.Add(file);

    foreach (var dir in directory.GetDirectories())
        GetAllFiles(dir, list);
}

static string ComputeHash(FileInfo file)
{
    try
    {
        using var stream = file.OpenRead();
        using var sha = SHA256.Create();
        byte[] hash = sha.ComputeHash(stream);
        return Convert.ToHexString(hash);
    }
    catch
    {
        return null;
    }
}
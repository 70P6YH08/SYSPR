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
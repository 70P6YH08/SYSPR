using System.Drawing;
using System.Drawing.Imaging;

object locker = new();
DirectoryInfo picturesDir = new(@"C:\Users\-221\Students\ispp31\SYSPR\Labs\LabWork23\Lab23\Lab23\Pictures\");

GetPictures();

void GetPictures()
{
    string newDirPath = Path.Combine(picturesDir.Parent.FullName, $"{picturesDir.Name}_inverted");
    if (!Directory.Exists(newDirPath))
        Directory.CreateDirectory(newDirPath);

    int totalFiles = picturesDir.GetFiles().Length;
    int completedCount = 0;

    ParallelLoopResult result = Parallel.ForEach<FileInfo>(picturesDir.GetFiles(), file =>
    {
        if (file.Exists)
        {
            if (isPicture(file.Extension))
            {
                try
                {
                    using Bitmap bitmap = new(file.FullName);

                    int imageWidth = bitmap.Width;
                    int imageHeight = bitmap.Height;

                    for (int x = 0; x < imageWidth; x++)
                    {
                        for (int y = 0; y < imageHeight; y++)
                        {
                            var color = bitmap.GetPixel(x, y);
                            int red = 255 - color.R;
                            int green = 255 - color.G;
                            int blue = 255 - color.B;
                            var invertedColor = Color.FromArgb(red, green, blue);
                            bitmap.SetPixel(x, y, invertedColor);
                        }
                    }
                    string newPictName = Path.Combine(newDirPath, $"inverted_{file.Name}");

                    if (!File.Exists(newPictName))
                        bitmap.Save(newPictName, ImageFormat.Png);

                    GetProgress(Interlocked.Increment(ref completedCount), totalFiles);
                }
                catch (ArgumentException ex) { }
                catch (InvalidOperationException ex) { }
            }
        }
    });
}

void GetProgress(int incFile, int allFiles)
{
    lock (locker)
    {
        int maxDivision = 100;
        int percent = incFile * maxDivision / allFiles;
        int sizeProgressString = 20;

        int filled = percent * sizeProgressString / maxDivision;

        string progressString = new string('=', filled) + new string('-', sizeProgressString - filled);

        Console.Write($"\r[{progressString}] {percent}%");
    }
}

bool isPicture(string fileExt)
{
    bool isPicture = false;
    switch (fileExt)
    {
        case ".jpg" or ".jpeg" or ".bmp" or ".png" or ".PNG" or ".JPG" or ".JPEG" or ".BMP":
            isPicture = true;
            break;
    }
    return isPicture;
}
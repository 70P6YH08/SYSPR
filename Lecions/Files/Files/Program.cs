using System.IO.Compression;

foreach (DriveInfo drive in DriveInfo.GetDrives())
{
    Console.WriteLine($"{drive.Name} " +
        $"{drive.VolumeLabel} " +
        $"{drive.DriveFormat} " +
        $"{drive.DriveType} " +
        $"{drive.TotalSize >> 30} " +
        $"{drive.AvailableFreeSpace >> 30}");
}

//ZipFile.CreateFromDirectory("C:\\Users\\221\\!!!Students\\ISPP-55",
//    "C:\\Users\\221\\!!!Students\\ISPP-55.zip");
//ZipFile.ExtractToDirectory("C:\\Users\\221\\!!!Students\\ISPP-55.zip",
//    "C:\\Users\\221\\!!!Students\\ISPP-56");

//using (var archive = ZipFile.Open("C:\\Users\\221\\!!!Students\\ISPP-55", ZipArchiveMode.Update))
//{
//    var entry = archive.GetEntry("");
//    entry.ExtractToFile("");
//}

using (FileStream fs = new FileStream("binary.dat", FileMode.Create))
{
    byte[] buffer = { 1, 2, 3, 4, 5 };
    fs.Write( buffer, 0, buffer.Length );
}

using (FileStream fs = new FileStream("binary.dat", FileMode.Open))
{
    byte[] buffer = new byte[5];
    fs.Read(buffer, 0, buffer.Length);
    Console.WriteLine(string.Join(", ", buffer));
}
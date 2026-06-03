public class FileDirInfo
{
    public FileType Type { get; set; } = 0;
    public string? IconPath { get => field = $@"C:\Users\gorbu\Desktop\All\collage\3 year 2 semestr\SYSPR\Labs\LabWork17\LabWork17\LabWork17\Icons\{Type}.png"; set; }
    public string Name { get; set; }
    public DateTime? ChangedTime { get; set; }
    public string? Weight { get; set; }

    public string DiskLetter { get; set; }
    public double DiskTotalSize { get; set; }
    public string? DiskTotalSizeText { get; set; }
    public string? DiskTotalSizeTextBytes { get; set; }
    public string? DiskAvailableSize { get; set; }
    public string? DiskAvailableSizeBytes { get; set; }
    public double DiskOccupiedSize { get; set; }
    public string? DiskOccupiedSizeText { get; set; }
    public string? DiskOccupiedSizeBytes { get; set; }
    public string? DiskType { get; set; }
    public TypeUnitWeight UnitWeight { get; set; } = 0;
    public string? DiskFileSystem { get; set; }

    public int CountFiles { get; set; }
    public int CountDirs { get; set; }
    public string? FullDirPath { get; set; }
    public string? FolderSize { get; set; }
    public double FolderPercent { get; set; }
    public Dictionary<string, string> TopFiles { get; set; }


    public enum FileType
    {
        Archive = 1,
        Audio = 2,
        Folder = 3,
        ParentFolder = 4,
        Image = 5,
        Text = 6,
        Video = 7,
        HardDrive = 8,
        Unknown = 9,
    }

    public enum TypeUnitWeight
    {
        B = 0,
        KB = 1,
        MB = 2,
        GB = 3,
        TB = 4
    }
}
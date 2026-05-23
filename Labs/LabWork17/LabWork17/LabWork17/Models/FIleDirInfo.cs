public class FileDirInfo
{
    public FileType Type { get; set; } = 0;
    public string? IconPath { get => field = $@"C:\Users\-221\Students\ispp31\SYSPR\Labs\LabWork17\LabWork17\LabWork17\Icons\{Type}.png"; set; }
    public string Name { get; set; }
    public DateTime? ChangedTime { get; set; }
    public string? Weight { get; set; }

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
}
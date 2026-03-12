
using Microsoft.Win32;

string path = "SOFTWARE\\LectionApp";

//SaveData(12);
//Console.WriteLine(LoadData());
DeleteData();

void SaveData(int data)
{
    using(RegistryKey key = Registry.CurrentUser.CreateSubKey(path))
    {
        key.SetValue("Data", data);
    }
}

int LoadData()
{
    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(path))
    {
        return (int)key.GetValue("Data");
    }
}

void DeleteData()
{
    Registry.CurrentUser.DeleteSubKey(path);
}
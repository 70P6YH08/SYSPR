using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

class JsonSerializeFunction
{
    public static readonly string jsonUsersInfoPath = "person.json";

    public static JsonSerializerOptions options = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        WriteIndented = true
    };

    public static void SerializeUsersInfo(List<Person> persons, JsonSerializerOptions options)
    {
        using (FileStream fileStream = new(jsonUsersInfoPath, FileMode.OpenOrCreate))
            JsonSerializer.Serialize<List<Person>>(fileStream, persons, options);
    }

    public static List<Person> DeserializeUsersInfo()
    {
        using (FileStream fileStream = new(jsonUsersInfoPath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<List<Person>>(fileStream);
        }
    }
}

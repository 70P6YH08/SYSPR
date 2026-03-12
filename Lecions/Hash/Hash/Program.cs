
using System.Security.Cryptography;
using System.Text;

byte[] key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");
var message = "Hello, Kim!";

var encrypted = (Encrypt(key, iv, message));
Console.WriteLine(encrypted);
Console.WriteLine(Decrypt(key, iv, encrypted));

static string Encrypt(byte[] key, byte[] iv, string message)
{
    using (Aes aes = Aes.Create())
    {
        aes.Key = key;
        aes.IV = iv;

        ICryptoTransform encryptor = aes.CreateEncryptor();

        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(
                                            ms,
                                            encryptor,
                                            CryptoStreamMode.Write)) //Если шифруем, то пишем
            {
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(message);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}

static string Decrypt(byte[] key, byte[] iv, string message)
{
    using (Aes aes = Aes.Create())
    {
        aes.Key = key;
        aes.IV = iv;

        ICryptoTransform decryptor = aes.CreateDecryptor();

        using (MemoryStream ms = new MemoryStream(
            Convert.FromBase64String(message)))
        {
            using (CryptoStream cs = new CryptoStream(
                                            ms,
                                            decryptor,
                                            CryptoStreamMode.Read)) //Если ДЕшифруем, то читаем
            {
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}

Console.WriteLine(
    Convert.ToBase64String(
        SHA256.HashData(
            Encoding.UTF8.GetBytes(message)
            )
        )
    );

var sha = SHA256.Create();

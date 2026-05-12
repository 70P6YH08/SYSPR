using System.Text.Json;

class UserFunctions
{
    public static string Password(out ConsoleKeyInfo key)
    {
        string password = "";
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            while (String.IsNullOrEmpty(password))
            {
                Console.Write("Пароль не может быть пуст: ");
                password = Console.ReadLine();
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }

    public static string Login()
    {
        Console.Write("Введите логин: ");
        string login = Console.ReadLine();
        while (String.IsNullOrEmpty(login))
        {
            Console.Write("Логин не может быть пуст: ");
            login = Console.ReadLine();
        }
        return login;
    }

    public static void UserAuthorization(string currentLogin, string currentPassword)
    {
        //List<Person> persons = new List<Person>();
        //JsonSerializeFunction.SerializeUsersInfo(persons, JsonSerializeFunction.options);

        FileInfo fileInfo = new(JsonSerializeFunction.jsonUsersInfoPath);

        string success = "Вы вошли!";
        string login = "";
        string password = "";
        ConsoleKeyInfo key;
        bool flag = false;
        int fail = 0;

        if (fileInfo.Exists)
        {
            var fileText = JsonSerializeFunction.DeserializeUsersInfo();

            while (flag != true && fail != 10)
            {
                if (String.IsNullOrEmpty(currentLogin))
                    login = Login();
                else
                    login = currentLogin;

                if (fileText.Exists(p => p.Login == login))
                {
                    if (String.IsNullOrEmpty(currentPassword))
                    {
                        Console.Write("Введите пароль: ");
                        password = Password(out key);
                    }
                    else
                        password = currentPassword;

                    if (fileText.Exists(p => p.Password == password && p.Login == login))
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        int n = 3;
                        while (n > 0)
                        {
                            Console.Write($"Повторно введите пароль ({n}): ");
                            password = Password(out key);

                            if (fileText.Exists(p => p.Password == password && p.Login == login))
                            {
                                flag = true;
                                break;
                            }
                            n--;
                        }
                        password = "";
                    }
                }
                else
                {
                    fail++;
                    Console.WriteLine("Такого пользователя не существует!");
                }
            }
        }
        else
            Console.WriteLine("Файл не найден!");

        if (flag == true)
            Console.WriteLine(success);
    }
    public static void UserRegister(string currentLogin, string currentPassword)
    {
        FileInfo fileInfo = new FileInfo(JsonSerializeFunction.jsonUsersInfoPath);

        string login = "";
        ConsoleKeyInfo key;
        string password = "";
        string retryPassword = "";

        if (fileInfo.Exists)
        {
            var fileText = JsonSerializeFunction.DeserializeUsersInfo();

            while (true)
            {
                if (String.IsNullOrEmpty(currentLogin))
                    login = Login();
                else
                    login = currentLogin;

                if (!fileText.Exists(p => p.Login == login))
                {
                    if (String.IsNullOrEmpty(currentPassword))
                    {
                        Console.Write("Введите пароль: ");
                        password = Password(out key);

                        Console.Write("Подтвердите пароль: ");
                        retryPassword = Password(out key);
                    }
                    else
                    {
                        password = currentPassword;
                        retryPassword = currentPassword;
                    }

                    if (password.Equals(retryPassword))
                    {
                        Person newPerson = new() { Login = login, Password = password };
                        fileText.Add(newPerson);
                        JsonSerializeFunction.SerializeUsersInfo(fileText, JsonSerializeFunction.options);
                        Console.WriteLine("Вы успешно зарегистировались!");
                        break;
                    }
                    else
                        Console.WriteLine("Пароли не совпадают!");
                }
                else
                    Console.WriteLine("Такой логин уже занят");
            }
        }
        else
            Console.WriteLine("Файл не найден");
    }
}

using System.CommandLine;

Option<string> optionLogin = new("--login", "-l", "-L")
{
    Description = "Логин",
    Required = false
};

Option<string> optionPassword = new("--pwd", "-p", "-P")
{
    Description = "Пароль",
    Required = false
};

RootCommand rootCommand = new RootCommand("16 лаба")
{
    optionLogin,
    optionPassword
};

rootCommand.SetAction(res =>
{
    UserFunctions.UserAuthorization(
        res.GetValue(optionLogin),
        res.GetValue(optionPassword)
        );
});

Command regCommand = new("register", "Регистрация пользователя")
{
    optionLogin,
    optionPassword
};

regCommand.SetAction(res =>
{
    UserFunctions.UserRegister(
        res.GetValue(optionLogin),
        res.GetValue(optionPassword)
        );
});

rootCommand.Subcommands.Add(regCommand);

var result = rootCommand.Parse(args);
result.Invoke();


//using Microsoft.AspNetCore.SignalR.Client;

//var connection = new HubConnectionBuilder()
//    .WithUrl("https://localhost:7058/chat")
//    .Build();

//Console.Write("Введите имя: ");
//string name = Console.ReadLine();

//while (String.IsNullOrWhiteSpace(name))
//{
//    Console.Write("Имя не может быть пустым: ");
//    name = Console.ReadLine();
//}

//connection.On<Task>("RetransmissionAsync_callback", () =>
//{
//    Console.WriteLine();
//});

//await connection.StartAsync();
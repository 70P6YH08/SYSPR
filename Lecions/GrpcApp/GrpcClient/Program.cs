using Grpc.Net.Client;
using GrpcClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5023");

var client = new Greeter.GreeterClient(channel);

Console.WriteLine("Введите имя:");
string? name = Console.ReadLine();
var reply = await client.SayHelloAsync(new HelloRequest
{
    Name = name
});
Console.WriteLine($"Ответ сервера: {reply.Message}");
Console.ReadKey();
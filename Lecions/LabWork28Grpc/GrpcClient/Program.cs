using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcOrderClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5166");
var client = new OrderManager.OrderManagerClient(channel);

bool exit = false;
while (!exit)
{
    Console.WriteLine("\n   Управление заказами    ");
    Console.ResetColor();
    Console.WriteLine("1. Создать новый заказ");
    Console.WriteLine("2. Показать все заказы");
    Console.WriteLine("3. Найти заказ по ID");
    Console.WriteLine("4. Удалить заказ");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");

    var choice = Console.ReadLine();
    try
    {
        switch (choice)
        {
            case "1":
                await CreateOrderAsync(client);
                break;
            case "2":
                await ListOrdersAsync(client);
                break;
            case "3":
                await GetOrderAsync(client);
                break;
            case "4":
                await DeleteOrderAsync(client);
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                break;
        }
    }
    catch (RpcException ex)
    {
        Console.WriteLine($"Ошибка gRPC: {ex.Status.Detail} (Код: {ex.StatusCode})");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
        Console.ResetColor();
    }
}

async Task CreateOrderAsync(OrderManager.OrderManagerClient client)
{
    var request = new CreateOrderRequest();
    Console.WriteLine("\n--- Создание заказа ---");

    while (true)
    {
        Console.Write("Введите наименование товара (или нажмите Enter для завершения): ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) break;

        Console.Write("Введите цену товара: ");
        if (!double.TryParse(Console.ReadLine(), out double price))
        {
            Console.WriteLine("Некорректная цена. Товар не добавлен.");
            continue;
        }

        request.Items.Add(new OrderItem { Name = name, Price = price });
    }

    if (request.Items.Count == 0)
    {
        Console.WriteLine("Нельзя создать пустой заказ.");
        return;
    }

    var result = await client.CreateOrderAsync(request);
    Console.WriteLine($"Заказ успешно создан! ID: {result.Id} от {result.OrderDate.ToDateTime().ToLocalTime()}");
    Console.ResetColor();
}

async Task ListOrdersAsync(OrderManager.OrderManagerClient client)
{
    Console.WriteLine("\n--- Список всех заказов ---");
    var response = await client.ListOrdersAsync(new Empty());
    PrintOrders(response.Orders);
}

async Task GetOrderAsync(OrderManager.OrderManagerClient client)
{
    Console.Write("\nВведите ID заказа для поиска: ");
    string id = Console.ReadLine();

    var order = await client.GetOrderAsync(new GetOrderRequest { Id = id });
    PrintOrderDetails(order);
}

async Task DeleteOrderAsync(OrderManager.OrderManagerClient client)
{
    Console.Write("\nВведите ID заказа для удаления: ");
    string id = Console.ReadLine();

    await client.DeleteOrderAsync(new DeleteOrderRequest { Id = id });
    Console.WriteLine("Заказ успешно удален.");
    Console.ResetColor();
}

void PrintOrders(IEnumerable<Order> orders)
{
    if (!orders.Any())
    {
        Console.WriteLine("Заказы отсутствуют.");
        return;
    }

    foreach (var order in orders)
    {
        PrintOrderDetails(order);
    }
}

void PrintOrderDetails(Order order)
{
    double total = 0;
    Console.WriteLine();
    Console.WriteLine($"ID заказа: {order.Id}");
    Console.WriteLine($"Дата:      {order.OrderDate.ToDateTime().ToLocalTime()}");
    Console.WriteLine("Состав:");
    foreach (var item in order.Items)
    {
        Console.WriteLine($"  - {item.Name}: {item.Price:C}");
        total += item.Price;
    }
    Console.WriteLine($"Итоговая стоимость: {total:C}");
}

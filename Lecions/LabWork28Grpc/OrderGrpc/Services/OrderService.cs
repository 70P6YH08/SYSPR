using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcOrderService.Services;

public class OrderService : OrderManager.OrderManagerBase
{
    private static readonly Dictionary<string, Order> _orders = new();

    public override Task<Order> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        var newOrder = new Order
        {
            Id = Guid.NewGuid().ToString(),
            OrderDate = Timestamp.FromDateTime(DateTime.UtcNow),
            Items = { request.Items }
        };

        _orders[newOrder.Id] = newOrder;
        return Task.FromResult(newOrder);
    }

    public override Task<Order> GetOrder(GetOrderRequest request, ServerCallContext context)
    {
        if (_orders.TryGetValue(request.Id, out var order))
        {
            return Task.FromResult(order);
        }
        throw new RpcException(new Status(StatusCode.NotFound, $"Order with ID {request.Id} not found."));
    }

    public override Task<Empty> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
    {
        if (!_orders.Remove(request.Id))
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Order with ID {request.Id} not found."));
        }
        return Task.FromResult(new Empty());
    }

    public override Task<OrderList> ListOrders(Empty request, ServerCallContext context)
    {
        var response = new OrderList();
        response.Orders.AddRange(_orders.Values);
        return Task.FromResult(response);
    }
}
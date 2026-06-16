using Grpc.Core;

namespace LabWork28Grpc.Services
{
    public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
    {
        public override Task<OrderReply> SayHello(OrderRequest request, ServerCallContext context)
        {
            logger.LogInformation("The message is received from {Name}", request.Name);

            return Task.FromResult(new OrderReply
            {
                Message = "Hello " + request.Name,
            });
        }
    }
}

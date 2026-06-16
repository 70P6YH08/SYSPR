using Grpc.Core;

namespace GrpcServer.Services
{
    public class OrderService(ILogger<OrderService> logger) : OrderSevice
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            logger.LogInformation("The message is received from {Name}", request.Name);

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}

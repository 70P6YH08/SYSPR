List<int> channel = new();
bool done = false;
Thread producer = new(ProducerFunc);

producer.Start();

for (int i = 0; i < 3; i++)
{
    Thread consumer = new(ConsumerFunc);
    var consumerId = consumer.ManagedThreadId;
    consumer.Start(consumerId);
}

void ProducerFunc()
{
    Random random = new Random();
    for (int i = 1; i <= 20; i++)
    {
        channel.Add(random.Next(1, 20));
        Thread.Sleep(200);
    }
    done = true;
}

void ConsumerFunc(object? consumerId)
{
    int product = 0;
    while (!done)
    {
        if(channel.Count > 0)
        {
            product = channel[channel.Count - 1];
            Console.WriteLine($"Потребитель {consumerId}: {product}");
            Thread.Sleep(200);
        }
    }
}
using Confluent.Kafka;
using Newtonsoft.Json;
using Novus.API.Models;

namespace Employee.API.Kafka;

public class KafkaNovusConsumer() : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _ = ConsumeAsync("rute-completed", stoppingToken);
        }, stoppingToken);
    }

    public Task ConsumeAsync(string topic, CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "employee-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);

            var novusdata = JsonConvert.DeserializeObject<Novusdatatest>(consumeResult.Message.Value);

            if (novusdata != null)
            {
                Console.WriteLine(novusdata.ToString());
            }
        }
        consumer.Close();
        return Task.CompletedTask;
    }
}

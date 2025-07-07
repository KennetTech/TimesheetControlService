using Confluent.Kafka;
using Newtonsoft.Json;
using Novus.API.Models;


namespace Fleet.API.Kafka;

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
            GroupId = "fleet-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);

            var novusdata = JsonConvert.DeserializeObject<Novusdata>(consumeResult.Message.Value);

            if (novusdata != null)
            {
                //check for vehicle id and what GPS data is for that vehicle on that date.
                Console.WriteLine(novusdata.ToString());
            }
        }
        consumer.Close();
        return Task.CompletedTask;
    }
}

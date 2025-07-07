using Confluent.Kafka;
using Timesheet.API.Data;
using Newtonsoft.Json;

namespace TimeCalcService.Kafka;

public class KafkaConsumer() : BackgroundService
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
            GroupId = "timesheet-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);

            var novusdata = JsonConvert.DeserializeObject<TimesheetModeltest>(consumeResult.Message.Value);

            if (novusdata != null)
            {
                TimesheetModeltest timesheet = new();
                timesheet = novusdata;
                Console.WriteLine(timesheet.ToString());
            }
        }
        consumer.Close();
        return Task.CompletedTask;
    }
}

using Confluent.Kafka;
using Control.API.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;


namespace Control.API.Kafka;

public class KafkaNovusConsumer() : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _ = ConsumeAsync("rute-completed", stoppingToken);
        }, stoppingToken);
    }

    public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "control-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);



            var novusdata = JsonConvert.DeserializeObject<Control.API.Models.Novusdata>(consumeResult.Message.Value);

            if (novusdata != null)
            {
                Console.WriteLine(novusdata.ToString());
                //DashboardEntry dashboardEntry = new DashboardEntry();
                //dashboardEntry.Novusdata = novusdata;

                //await hubContext.Clients.All.SendAsync("ReceiveDashboardEntry", novusdata);

                //check if rute is already created
                //Console.WriteLine("Data added to Server: ", novusdata.ToString());
                return;
            }
        }
        consumer.Close();
        return;
    }
}

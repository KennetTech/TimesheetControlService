using Confluent.Kafka;

namespace NovusService.Producer;

public class KafkaProducer: IkafkaProducer
{
    private readonly IProducer<string, string> _producer;
    public KafkaProducer()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public Task ProduceAsync(string topic,Message<string, string> message)
    {
        return _producer.ProduceAsync(topic, message);
    }
}

public interface IkafkaProducer
{
    Task ProduceAsync(string topic, Message<string, string> message);
}
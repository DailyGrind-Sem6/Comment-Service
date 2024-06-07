using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Comment_Service.Kafka;

public class KafkaProducer : IKafkaProducer
{
    private readonly ILogger<KafkaProducer> _logger;
    private readonly IConfiguration _configuration;
    private IProducer<Null, string> _producer;
    
    public KafkaProducer(ILogger<KafkaProducer> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        var kafkaBootstrapServer = _configuration.GetSection("Kafka:BootstrapServers").Value; 
        
        var config = new ProducerConfig()
        {
            BootstrapServers = kafkaBootstrapServer
        };
        _producer = new ProducerBuilder<Null, string>(config).Build();
        _logger.LogInformation("Connected to Kafka server: " + kafkaBootstrapServer);
    }

    public async Task SendMessage(string value, string topic)
    {
        await _producer.ProduceAsync(topic, new Message<Null, string>()
        {
            Value = value
        });
        _logger.LogInformation($"Message sent to Kafka: {value} - on topic: {topic}");
    }
}
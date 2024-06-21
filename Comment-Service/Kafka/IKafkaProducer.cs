namespace Comment_Service.Kafka;

public interface IKafkaProducer
{
    public Task SendMessage(string value, string topic);
}
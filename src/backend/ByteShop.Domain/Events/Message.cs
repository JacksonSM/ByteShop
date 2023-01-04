namespace ByteShop.Domain.Events;
public abstract class Message
{
    public string MessageType { get; private set; }
    public int AggregateId { get; set; }

    public Message()
    {
        MessageType = GetType().Name;
    }
}

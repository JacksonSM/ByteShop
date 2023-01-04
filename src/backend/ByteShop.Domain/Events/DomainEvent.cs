namespace ByteShop.Domain.Events;
public class DomainEvent : Event
{
    public DomainEvent(int aggregateId)
    {
        AggregateId = aggregateId;
    }
}

using MediatR;

namespace ByteShop.Domain.Events;
public abstract class Event : Message, INotification
{
    public DateTime Timestamp { get; set; }

    public Event()
    {
        Timestamp = DateTime.Now;
    }
}

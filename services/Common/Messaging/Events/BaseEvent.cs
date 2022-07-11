namespace Messaging.Events;

public class BaseEvent
{
    public BaseEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }
    
    public BaseEvent(Guid id, DateTime createdDate)
    {
        Id = id;
        CreatedDate = createdDate;
    }

    public Guid Id { get; }

    public DateTime CreatedDate { get; }
}
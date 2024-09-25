namespace EventProcessor;

public enum EventMessageType
{
    REQUEST,
    RESPONSE
}

public class EventMessageInstance
{
    public string TransactionId;
    public EventMessageType Type;
    public string Operation;
    public int Timestamp;
    public bool Success;
}
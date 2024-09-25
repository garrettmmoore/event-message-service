
export enum EventMessageType
{
    REQUEST,
    RESPONSE
}

export class EventMessageInstance
{
    public TransactionId: string;
    public Type: EventMessageType;
    public Operation: string;
    public Timestamp: number;
    public Success: boolean;
    constructor(transactionId: string, type: EventMessageType, operation: string, timestamp: number, Success: boolean){
        this.TransactionId = transactionId;
        this.Type = type;
        this.Operation = operation;
        this.Timestamp = timestamp;
        this.Success = Success;
    }
}
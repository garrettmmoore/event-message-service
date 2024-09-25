from enum import Enum, auto


class EventMessageType(Enum):
    REQUEST = 0,
    RESPONSE = 1


class EventMessageInstance:
    def __init__(
        self,
        transaction_id: str,
        message_type: EventMessageType,
        operation: str,
        timestamp: int,
        success: bool

    ):
        self.transaction_id = transaction_id
        self.message_type = message_type
        self.operation = operation
        self.timestamp = timestamp
        self.success = success

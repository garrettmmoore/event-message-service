from src.EventProcessor.EventMessageInstance import EventMessageInstance, EventMessageType

class EventHandler:
    def get_number_processed_requests(self, events: [EventMessageInstance]) -> int:
        """
        A method to find the number of pending requests from a given batch of events.
        :param events: A list of event messages.
        :return: The total number of pending requests.
        """
        pending_requests = dict()

        for current_event in events:
            self.update_pending_requests(current_event, pending_requests)

        return len(pending_requests)
    

    def get_number_processed_requests_by_operation_name(
        self,
        events: [EventMessageInstance],
        operation_name: str
    ) -> int:
        """
         A method to find the number of pending requests from a given batch of events with the given operation name.
        :param events: A list of event messages.
        :param operation_name: The name of the operation to filter by.
        :return: The total number of pending requests.
        """
        pending_requests = dict()

        for current_event in events:
            if current_event.operation != operation_name: continue
            self.update_pending_requests(current_event, pending_requests)

        return len(pending_requests)

    def update_pending_requests(self, current_event: EventMessageInstance, pending_requests: dict):
        transaction_id: str = current_event.transaction_id

        if current_event.message_type == EventMessageType.REQUEST:
            if transaction_id not in pending_requests:
                pending_requests[transaction_id] = 0
        elif current_event.message_type == EventMessageType.RESPONSE and transaction_id in pending_requests:
            del pending_requests[transaction_id]

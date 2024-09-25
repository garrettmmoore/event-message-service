from src.EventProcessor.EventMessageInstance import EventMessageInstance, EventMessageType

class EventHandler:
    def get_number_processed_requests(self, events):
        """
        A method to find the number of pending requests from a given batch of events.

        :param events: A list of event messages.
        :return: The total number of pending requests.
        """
        # Use a dictionary to track the state of each transaction_id
        pending_request_counts = dict()

        for current_event in events:
            self.update_pending_requests(current_event, pending_request_counts)

        # Return the total count of pendings (only positive request counts remain pending)
        return sum(1 for count in pending_request_counts.values() if count > 0)

    def get_number_pending_requests_by_operation_name(self, events, operation_name):
        """
        A method to find the number of pending requests from a given batch of events with the given operation name.
        :param events: A list of event messages.
        :param operation_name: The name of the operation to filter by.
        :return: The total number of pending requests.
        """
        request_counts = {}

        for current_event in events:
            # Only proceed if current operation matches given operation name
            if current_event.operation == operation_name:
                self.update_pending_requests(current_event, request_counts)

        # Return the total count of pendings (only positive request counts remain pending)
        return sum(1 for count in request_counts.values() if count > 0)

    def update_pending_requests(self, current_event, pending_request_counts):
        """
        A helper method to update the state of pending requests.
        :param current_event: The event message containing the details of the request.
        :param pending_request_counts: A dictionary containing the count of pending requests.
        """
        transaction_id = current_event.transaction_id
        
        # If Request type, then increase the pending count for a given transactionId
        if current_event.message_type == EventMessageType.REQUEST:
            if transaction_id not in pending_request_counts:
                pending_request_counts[transaction_id] = 0
            pending_request_counts[transaction_id] += 1
        # Otherwise, if Response type, then decrease the pending count for a given transactionId
        elif current_event.message_type == EventMessageType.RESPONSE:
            if transaction_id not in pending_request_counts:
                pending_request_counts[transaction_id] = 0
            pending_request_counts[transaction_id] -= 1

            # If the count becomes zero, we have even pairs of REQUEST-RESPONSE, remove it
            if pending_request_counts[transaction_id] == 0:
                del pending_request_counts[transaction_id]

import { EventMessageInstance, EventMessageType } from './EventMessageInstance';

export class EventHandler {
    /**
     * A method to find the number of pending requests from a given batch of events.
     * @param events A list of event messages.
     * @returns The total number of pending requests.
     */
    getNumberPendingRequests(events: EventMessageInstance[]): number {
        // Initialize a hashmap to store the pending requests
        const pendingRequests = new Map<string, number>();

        // Iterate through the events and update the pending requests
        events.forEach(currentEvent => {
            this.updatePendingRequests(currentEvent, pendingRequests);
        });

        // Return the total count of the remaining pending requests
        return pendingRequests.size;
    }

    /**
     * A method to find the number of pending requests from a given batch of events
     * with the given operation name.
     * @param events A list of event messages.
     * @param operationName The name of the operation to filter by.
     * @returns The total number of pending requests.
     */
    getNumberPendingRequestsByOperationName(
        events: EventMessageInstance[],
        operationName: string
    ): number {
        const pendingRequests = new Map<string, number>();

        // Iterate through the events and update the pending requests
        events.forEach(currentEvent => {
            // Only proceed if current operation matches given operation name
            if (currentEvent.Operation != operationName) return;
            this.updatePendingRequests(currentEvent, pendingRequests);
        });

        // Return the total count of the remaining pending requests
        return pendingRequests.size;
    }

    /**
     * A helper method to refresh the pending requests.
     * @param currentEvent The event message containing the details of the request.
     * @param pendingRequests A map containing the pending requests.
     */
    private updatePendingRequests(
        currentEvent: EventMessageInstance,
        pendingRequests: Map<string, number>
    ) {
        const transactionId = currentEvent.TransactionId;

        // If the event is a REQUEST, add the request to the map
        if (currentEvent.Type == EventMessageType.REQUEST) {
            if (!pendingRequests.has(transactionId)) {
                pendingRequests.set(transactionId, 0);
            }
        }
        // Otherwise, if the event is a RESPONSE and a matching transactionId is found
        // in the map, the request has completed and is no longer pending. Delete the
        // matching request from the map.
        else if (
            currentEvent.Type == EventMessageType.RESPONSE &&
            pendingRequests.has(transactionId)
        ) {
            pendingRequests.delete(transactionId);
        }
    }
}

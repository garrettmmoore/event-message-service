import { EventMessageInstance, EventMessageType } from './EventMessageInstance';

export class EventHandler {
    /**
     * A method to find the number of pending requests from a given batch of events.
     * @param events A list of event messages.
     * @returns The total number of pending requests.
     */
    getNumberPendingRequests(events: EventMessageInstance[]): number {
        // Use a Map to track the state of each transactionId
        const requestCounts = new Map<string, number>();

        for (const currentEvent of events) {
            this.updatePendingRequests(currentEvent, requestCounts);
        }

        // Return the total count of pendings (only positive request counts remain pending)
        let pendingCount = 0;
        requestCounts.forEach(count => {
            if (count > 0) pendingCount++;
        });
        return pendingCount;
    }

    /**
     * A method to find the number of pending requests from a given batch of events with the given operation name.
     * @param events A list of event messages.
     * @param operationName The name of the operation to filter by.
     * @returns The total number of pending requests.
     */
    getNumberPendingRequestsByOperationName(
        events: EventMessageInstance[],
        operationName: string
    ): number {
        const requestCounts = new Map<string, number>();

        for (const currentEvent of events) {
            // Only proceed if current operation matches given operation name
            if (currentEvent.Operation === operationName) {
                this.updatePendingRequests(currentEvent, requestCounts);
            }
        }

        // Return the total count of pending requests (only positive request counts remain pending)
        let pendingCount = 0;
        requestCounts.forEach(count => {
            if (count > 0) {
                pendingCount++;
            }
        });
        return pendingCount;
    }

    /**
     * A helper method to update the state of pending requests.
     * @param currentEvent The event message containing the details of the request.
     * @param requestCounts A Map containing the count of pending requests.
     */
    private updatePendingRequests(
        currentEvent: EventMessageInstance,
        requestCounts: Map<string, number>
    ): void {
        const transactionId = currentEvent.TransactionId;

        // If Request type, then increase the pending count for a given transactionId
        if (currentEvent.Type === EventMessageType.REQUEST) {
            if (!requestCounts.has(transactionId)) {
                requestCounts.set(transactionId, 0);
            }
            requestCounts.set(
                transactionId,
                requestCounts.get(transactionId)! + 1
            );
        }
        // If Response type, then decrease the pending count for a given transactionId
        else if (currentEvent.Type === EventMessageType.RESPONSE) {
            if (!requestCounts.has(transactionId)) {
                requestCounts.set(transactionId, 0);
            }
            requestCounts.set(
                transactionId,
                requestCounts.get(transactionId)! - 1
            );

            // If the count becomes zero, we have even pairs of REQUEST-RESPONSE, remove it
            if (requestCounts.get(transactionId) === 0) {
                requestCounts.delete(transactionId);
            }
        }
    }
}

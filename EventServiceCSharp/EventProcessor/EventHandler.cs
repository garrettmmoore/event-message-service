namespace EventProcessor;

public class EventHandler
{
    /// <summary> A method to find the number of pending requests from a given batch of events. </summary>
    /// <param name="events"> A list of event messages. </param>
    /// <returns> The total number of pending requests. </returns>
    public int GetNumberPendingRequests(EventMessageInstance[] events)
    {
        // Use a dictionary to track the state of each transactionId
        Dictionary<string, int> pendingRequestCounts = new();

        foreach (EventMessageInstance currentEvent in events)
        {
            UpdatePendingRequests(currentEvent, pendingRequestCounts);
        }

        // Return the total count of pending requests (only positive request counts remain pending)
        return pendingRequestCounts.Values.Count(count => count > 0);
    }

    /// <summary>
    /// A method to find the number of pending requests from a given batch of events with the given
    /// operation name.
    /// </summary>
    /// <param name="events"> A list of event messages. </param>
    /// <param name="operationName"> The name of the operation to filter by. </param>
    /// <returns> The total number of pending requests. </returns>
    public int GetNumberPendingRequestsByOperationName(EventMessageInstance[] events, string operationName)
    {
        Dictionary<string, int> requestCounts = new();

        foreach (EventMessageInstance currentEvent in events)
        {
            // Only proceed if current operation matches given operation name
            if (currentEvent.Operation == operationName)
            {
                UpdatePendingRequests(currentEvent, requestCounts);
            }
        }

        // Return the total count of pending requests (only positive request counts remain pending)
        return requestCounts.Values.Count(count => count > 0);
    }

    /// <summary> A helper method to update the state of pending requests. </summary>
    /// <param name="currentEvent"> The event message containing the details of the request. </param>
    /// <param name="pendingRequestCounts"> A dictionary containing the count of pending requests. </param>
    private void UpdatePendingRequests(EventMessageInstance currentEvent, Dictionary<string, int> pendingRequestCounts)
    {
        string transactionId = currentEvent.TransactionId;
        
        // If Request type, then increase the pending count for a given transactionId
        if (currentEvent.Type == EventMessageType.REQUEST)
        {
            pendingRequestCounts.TryAdd(transactionId, 0);
            pendingRequestCounts[transactionId]++;
        }
        // If Response type, then decrease the pending count for a given transactionId
        else if (currentEvent.Type == EventMessageType.RESPONSE)
        {
            pendingRequestCounts.TryAdd(transactionId, 0);
            pendingRequestCounts[transactionId]--;

            // If the count becomes 0, we have even pairs of REQUEST-RESPONSE, remove it
            if (pendingRequestCounts[transactionId] == 0)
            {
                pendingRequestCounts.Remove(transactionId);
            }
        }
    }
}
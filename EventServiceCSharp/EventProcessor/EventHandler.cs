namespace EventProcessor;

public class EventHandler
{
    /// <summary> A method to find the number of pending requests from a given batch of events. </summary>
    /// <param name="events"> A list of event messages. </param>
    /// <returns> The total number of pending requests. </returns>
    public int GetNumberPendingRequests(EventMessageInstance[] events)
    {
        // Initialize a dictionary to store the pending requests
        Dictionary<string, int> pendingRequests = new();

        // Iterate through the events and update the pending requests
        foreach (EventMessageInstance currentEvent in events)
        {
            UpdatePendingRequests(currentEvent, pendingRequests);
        }

        // Return the total count of the remaining pending requests
        return pendingRequests.Count;
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
        Dictionary<string, int> pendingRequests = new();

        // Iterate through the events and update the pending requests
        foreach (EventMessageInstance currentEvent in events)
        {
            // Only proceed if current operation matches given operation name
            if (currentEvent.Operation != operationName) continue;

            UpdatePendingRequests(currentEvent, pendingRequests);
        }

        // Return the total count of the remaining pending requests
        return pendingRequests.Count;
    }

    /// <summary> A helper method to refresh the pending requests. </summary>
    /// <param name="currentEvent"> The event message containing the details of the request. </param>
    /// <param name="pendingRequests"> A map containing the pending requests. </param>
    private void UpdatePendingRequests(EventMessageInstance currentEvent, Dictionary<string, int> pendingRequests)
    {
        string transactionId = currentEvent.TransactionId;

        if (currentEvent.Type == EventMessageType.REQUEST)
        {
            pendingRequests.TryAdd(transactionId, 0);
        }
        else if (currentEvent.Type == EventMessageType.RESPONSE && pendingRequests.ContainsKey(transactionId))
        {
            pendingRequests.Remove(transactionId);
        }
    }
}
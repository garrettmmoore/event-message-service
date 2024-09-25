# event-message-service

[Click to: Navigate to Source Code for C#](./EventServiceCSharp/EventProcessor/EventHandler.cs)

[Click to: Navigate to Source Code for Python](./EventServicePython/src/EventProcessor/EventHandler.py)

[Click to: Navigate to Source Code for Node.js / TypeScript](./EventServiceNodeJS/src/EventHandler.ts)


# Table of Contents

- [Installation](#installation)
- [Approach](#approach)
- [Complexity Analysis](#complexity-analysis)

## Installation

Clone the repository from GitHub by opening Terminal and running the following command:

```commandline
git clone https://github.com/garrettmmoore/event-message-service.git
```

## Approach

For the purposes of this example, we are only concerned with requests that have not yet been processed and are in the pending state.

We know a request is pending if there is no corresponding response with the same `transactionId` in the current batch of event messages.

We can leverage a data structure such as a Map or a Dictionary to keep track of the pending request count. By using an event message's `transactionId` as the key, we can quickly lookup to see if the current event message already exists in our Map.

If the current event is a `Request` type and does not exist in the Map, we can consider this Request as pending. If the current event is a `Response` type and the `transactionId` (the key) already exists in our Map, we can remove the Pending Request from the map as it has already been processed.

When keeping track of the counts, each `Request` message increases the pending count for a given `transactionId` and each `Response` decreases it. After iterating through all of the event messages, we return the total count of requests that have a positive request count. In other words, requests that are still pending do not have a corresponding response. The positive counts represent unresolved transactions. A count of zero would indicate that the `Requests` and `Responses` have been perfectly balanced. A negative count indicates there is an imbalance where there are more `Responses` than `Requests`.

Taking it one step further, to get the number of pending requests by operation name, we can simply filter out irrelevant requests during the events iteration by only proceeding to update the pending requests count if the current event's operation name is the same as the given operation name.

Assumptions given the sample event messages:
- In a given batch of event messages, there are no duplicate Requests and there are no duplicate Responses.
- A Response can exist without a corresponding Request.
- The list of event messages is unordered, meaning a `Response` can appear before it's corresponding `Request`.

## Complexity Analysis

### Time Complexity

The time complexity of both the GetNumberPendingRequests() and GetNumberPendingRequestsByOperationName() is O(n), where n is the number of events.

### Space Complexity
The space complexity of both GetNumberPendingRequests() and GetNumberPendingRequestsByOperationName() is O(n), where n is the number of events. The worst case would be where the dictionary stores an entry for each event, but the typical case would be where the dictionary stores entries only for the number of pending requests.
# event-message-service

[Click to: Navigate to Source Code for C#](./EventServiceCSharp/EventProcessor/EventHandler.cs)

[Click to: Run C# Solution in Browser](https://replit.com/@gmoorecode/event-message-service-csharp#main.cs)

[Click to: Navigate to Source Code for Python](./EventServicePython/src/EventProcessor/EventHandler.py)

[Click to: Run Python Solution in Browser](https://replit.com/@gmoorecode/event-message-service#main.py)

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

We know a request is pending if a corresponding response with the same `transactionId` doesn't exist in the current batch of event messages.

We can leverage a data structure such as a Map or a Dictionary to keep track of the pending requests. By using an event message's `transactionId` as the key, we can quickly lookup to see if the current event message already exists in our Map.

If the current event is a `Request` type and does not exist in the Map, we can consider this Request as pending. If the current event is a `Response` type and the transactionId (the key) already exists in our Map, we can remove the Pending Request from the map as it has already been processed.

Taking it one step further, to get the number of pending requests by operation name, we can simply filter out irrelevant requests by only proceeding during the events iteration if the current event's name is the same as the given operation name.

## Complexity Analysis

### Time Complexity

The time complexity of both the GetNumberPendingRequests() and GetNumberPendingRequestsByOperationName() is O(n), where n is the number of events.

### Space Complexity
The space complexity of both GetNumberPendingRequests() and GetNumberPendingRequestsByOperationName() is O(n), where n is the number of events. The worst case would be where the dictionary stores an entry for each event, but the typical case would be where the dictionary stores entries only for the number of pending requests.
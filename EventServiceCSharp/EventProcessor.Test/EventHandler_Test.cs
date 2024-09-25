namespace EventProcessor.Test;

[TestClass]
public class EventHandler_Test
{
    private EventMessageInstance[] eventMessageInstances = [];

    [TestInitialize]
    public void SetUp()
    {
        eventMessageInstances =
        [
            new EventMessageInstance
            {
                TransactionId = "01",
                Type = EventMessageType.REQUEST,
                Operation = "GetDataUsage",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "01",
                Type = EventMessageType.RESPONSE,
                Operation = "GetDataUsage",
                Timestamp = 1725392155,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "021",
                Type = EventMessageType.REQUEST,
                Operation = "Action",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "022",
                Type = EventMessageType.RESPONSE,
                Operation = "Action",
                Timestamp = 1725392155,
                Success = false
            },
            new EventMessageInstance
            {
                TransactionId = "031",
                Type = EventMessageType.REQUEST,
                Operation = "GetDataUsage",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "032",
                Type = EventMessageType.RESPONSE,
                Operation = "GetDataUsage",
                Timestamp = 1725392155,
                Success = false
            },
            new EventMessageInstance
            {
                TransactionId = "041",
                Type = EventMessageType.REQUEST,
                Operation = "Action",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "041",
                Type = EventMessageType.RESPONSE,
                Operation = "Action",
                Timestamp = 1725392155,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "051",
                Type = EventMessageType.REQUEST,
                Operation = "GetDataUsage",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "052",
                Type = EventMessageType.RESPONSE,
                Operation = "GetDataUsage",
                Timestamp = 1725392155,
                Success = false
            },
            new EventMessageInstance
            {
                TransactionId = "061",
                Type = EventMessageType.REQUEST,
                Operation = "Action",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "062",
                Type = EventMessageType.RESPONSE,
                Operation = "Action",
                Timestamp = 1725392155,
                Success = false
            },
            new EventMessageInstance
            {
                TransactionId = "071",
                Type = EventMessageType.REQUEST,
                Operation = "GetDataUsage",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "071",
                Type = EventMessageType.RESPONSE,
                Operation = "GetDataUsage",
                Timestamp = 1725392155,
                Success = false
            },
            new EventMessageInstance
            {
                TransactionId = "081",
                Type = EventMessageType.REQUEST,
                Operation = "GetDataUsage",
                Timestamp = 1725392080,
                Success = true
            },
            new EventMessageInstance
            {
                TransactionId = "082",
                Type = EventMessageType.RESPONSE,
                Operation = "GetDataUsage",
                Timestamp = 1725392155,
                Success = false
            }
        ];
    }

    [TestMethod]
    public void TestGetNumberPendingRequests()
    {
        Console.WriteLine("count: " + eventMessageInstances.Length);
        var eventService = new EventHandler();
        int result = eventService.GetNumberPendingRequests(eventMessageInstances);
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void TestGetNumberPendingRequestsByOperationName()
    {
        var eventService = new EventHandler();
        const string operationName = "GetDataUsage";
        int result = eventService.GetNumberPendingRequestsByOperationName(eventMessageInstances, operationName);
        Assert.AreEqual(3, result);
    }
}
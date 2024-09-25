import unittest
from src.EventProcessor.EventMessageInstance import EventMessageInstance, EventMessageType
from src.EventProcessor.EventHandler import EventHandler


class TestEventHandler(unittest.TestCase):
    def setUp(self):
        self.solution = EventHandler()
        self.eventMessageInstances = [
            EventMessageInstance("01", EventMessageType.REQUEST, "GetDataUsage", 175382080, True),
            EventMessageInstance("01", EventMessageType.RESPONSE, "GetDataUsage", 175382080, True),

            EventMessageInstance("021", EventMessageType.REQUEST, "Action", 175382080, True),
            EventMessageInstance("022", EventMessageType.RESPONSE, "Action", 175382080, True),

            EventMessageInstance("031", EventMessageType.REQUEST, "GetDataUsage", 175382080, True),
            EventMessageInstance("032", EventMessageType.RESPONSE, "GetDataUsage", 175382080, True),

            EventMessageInstance("041", EventMessageType.REQUEST, "Action", 175382080, True),
            EventMessageInstance("041", EventMessageType.RESPONSE, "Action", 175382080, True),

            EventMessageInstance("051", EventMessageType.REQUEST, "GetDataUsage", 175382080, True),
            EventMessageInstance("052", EventMessageType.RESPONSE, "GetDataUsage", 175382080, True),

            EventMessageInstance("061", EventMessageType.REQUEST, "Action", 175382080, True),
            EventMessageInstance("062", EventMessageType.RESPONSE, "Action", 175382080, True),

            EventMessageInstance("071", EventMessageType.REQUEST, "GetDataUsage", 175382080, True),
            EventMessageInstance("071", EventMessageType.RESPONSE, "GetDataUsage", 175382080, True),

            EventMessageInstance("081", EventMessageType.REQUEST, "GetDataUsage", 175382080, True),
            EventMessageInstance("082", EventMessageType.RESPONSE, "GetDataUsage", 175382080, True)
        ]

    def test_get_number_processed_requests(self):
        self.assertEqual(
            5,
            self.solution.get_number_processed_requests(self.eventMessageInstances)
        )

    def test_get_number_processed_requests_by_operation_name(self):
        self.assertEqual(
            3,
            self.solution.get_number_pending_requests_by_operation_name(
                self.eventMessageInstances,
                "GetDataUsage"
            )
        )


if __name__ == "__main__":
    unittest.main()

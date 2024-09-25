import { EventHandler } from './EventHandler';
import { EventMessageInstance, EventMessageType } from './EventMessageInstance';

const eventMessageInstances = [
    new EventMessageInstance(
        '01',
        EventMessageType.REQUEST,
        'GetDataUsage',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '01',
        EventMessageType.RESPONSE,
        'GetDataUsage',
        1725392155,
        true
    ),
    new EventMessageInstance(
        '021',
        EventMessageType.REQUEST,
        'Action',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '022',
        EventMessageType.RESPONSE,
        'Action',
        1725392155,
        false
    ),
    new EventMessageInstance(
        '031',
        EventMessageType.REQUEST,
        'GetDataUsage',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '032',
        EventMessageType.RESPONSE,
        'GetDataUsage',
        1725392155,
        false
    ),
    new EventMessageInstance(
        '041',
        EventMessageType.REQUEST,
        'Action',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '041',
        EventMessageType.RESPONSE,
        'Action',
        1725392155,
        true
    ),
    new EventMessageInstance(
        '051',
        EventMessageType.REQUEST,
        'GetDataUsage',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '052',
        EventMessageType.RESPONSE,
        'GetDataUsage',
        1725392155,
        false
    ),
    new EventMessageInstance(
        '061',
        EventMessageType.REQUEST,
        'Action',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '062',
        EventMessageType.RESPONSE,
        'Action',
        1725392155,
        false
    ),
    new EventMessageInstance(
        '071',
        EventMessageType.REQUEST,
        'GetDataUsage',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '071',
        EventMessageType.RESPONSE,
        'GetDataUsage',
        1725392155,
        false
    ),
    new EventMessageInstance(
        '081',
        EventMessageType.REQUEST,
        'GetDataUsage',
        1725392080,
        true
    ),
    new EventMessageInstance(
        '082',
        EventMessageType.RESPONSE,
        'GetDataUsage',
        1725392155,
        false
    )
];

test('Testing getNumberPendingRequests()', () => {
    const eventHandler = new EventHandler();
    expect(eventHandler.getNumberPendingRequests(eventMessageInstances)).toBe(
        5
    );
});

test('Testing GetNumberPendingRequestsByOperationName()', () => {
    const eventHandler = new EventHandler();
    expect(
        eventHandler.getNumberPendingRequestsByOperationName(
            eventMessageInstances,
            'GetDataUsage'
        )
    ).toBe(3);
});

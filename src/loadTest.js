import http from 'k6/http';
import { check, sleep } from 'k6';

let customerIdCounter = 1; 

export let options = {
    stages: [
        { duration: '1m', target: 100000 }, 
    ],
};

export default function () {
    let customerId = `Customer-${customerIdCounter++}`; 
    let res = http.post('http://localhost:6000/basket/add', JSON.stringify({
        CustomerId: customerId,
        ProductName: '12345',
        Quantity: 1,
        Price: 100.00
    }), { headers: { 'Content-Type': 'application/json' } });

    check(res, { 'status was 200': (r) => r.status === 200 });

    sleep(1);
}

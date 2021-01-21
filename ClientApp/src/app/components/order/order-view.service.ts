import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { OrderService } from "src/app/core/services/order.service";
import { DelivererRate, DishRate, Order } from "src/app/shared/models/order.interface";

@Injectable({
  providedIn: "root"
})
export class OrderViewService {

  constructor(private _orderService: OrderService) { }

  getUserOrders(): Observable<Order[]> {
    return this._orderService.getUserOrders();
  }

  getUserOrdersMock(): Observable<Order[]> {
    return this.createUserOrdersDataMock();
  }

  rateDeliverer(delivererRate: DelivererRate): void {
    this._orderService.addDelivererRate(delivererRate);
  }

  rateDish(dishRate: DishRate): void {
    this._orderService.addDishRate(dishRate);
  }

  createUserOrdersDataMock(): Observable<Order[]> {
    const today = new Date();
    const d1 = new Date();
    d1.setDate(d1.getDate() - 14);
    const d2 = new Date();
    d2.setDate(d2.getDate() - 14);
    d2.setMinutes(d2.getMinutes() + 35);

    let order: Order[] = [
      {
        id: 0,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 9,
        delivererId: 1,
        orderStation: {
          id: 1,
          city: "Ruda Śląska",
          address: "Teatralna 6b",
          restaurant: {
            id: 1,
            name: "Esencja Smaku",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 0,
            name: "rosół z kury",
            price: 10
          },
          {
            id: 0,
            name: "rosół z kury",
            price: 10
          },
          {
            id: 3,
            name: "pizza margaritta",
            price: 22
          },
        ]
      },
      {
        id: 1,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 2,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 3,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 4,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 5,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 6,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 7,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 8,
        delivererId: 0,
        orderStation: {
          id: 0,
          city: "Ruda śląska",
          address: "1 Maja 76",
          restaurant: {
            id: 0,
            name: "Wiśniowy Sad",
            food: []
          }
        },
        status: "ENDED",
        startTime: d1,
        endTime: d2,
        food: [
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 2,
            name: "pomidorowa",
            price: 12
          },
          {
            id: 1,
            name: "spaghetti",
            price: 15
          },
        ]
      },
      {
        id: 9,
        delivererId: 1,
        orderStation: {
          id: 1,
          city: "Ruda Śląska",
          address: "Teatralna 6b",
          restaurant: {
            id: 1,
            name: "Esencja Smaku",
            food: []
          }
        },
        status: "REALIZE",
        startTime: today,
        endTime: null,
        food: [
          {
            id: 0,
            name: "rosół z kury",
            price: 10
          },
          {
            id: 0,
            name: "rosół z kury",
            price: 10
          },
          {
            id: 3,
            name: "pizza margaritta",
            price: 22
          },
        ]
      }
    ]
    return of(order);
  }
}

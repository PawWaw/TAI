import { Injectable } from "@angular/core";
import { OrderService } from "src/app/core/services/order.service";
import { DishesInBasket } from "src/app/shared/models/dish.interface";
import { UserOrder } from "src/app/shared/models/order.interface";

@Injectable({
  providedIn: "root"
})
export class BasketViewService {

  constructor(private _orderService: OrderService) { }

  submitOrder(dishesInBasket: DishesInBasket[]): void {
    let order = new UserOrder();
    dishesInBasket.forEach((d: DishesInBasket) => {

      let elem = order.dishes.filter(x => x.orderStationId === d.dish.orderStationId);
      if (elem.length === 0) {
        order.dishes.push({
          orderStationId: d.dish.orderStationId,
          foods: [
            {
              dishId: d.dish.id,
              count: d.count
            }
          ]
        });
      }
      else {
        let index = order.dishes.indexOf(elem[0]);
        order.dishes[index].foods.push({dishId: d.dish.id, count: d.count});
      }
    });
    this._orderService.submitOrder(order).subscribe(() => {});
  }
}

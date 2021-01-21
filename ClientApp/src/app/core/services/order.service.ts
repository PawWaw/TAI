import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DelivererRate, DishRate, Order, UserOrder } from "src/app/shared/models/order.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";


@Injectable({
  providedIn: "root"
})
export class OrderService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  submitOrder(order: UserOrder): void {
    this._apiClientService.post(`${this._config.ApiUrl}/ClientApi/SubmitOrder`, { queryParams: order });
  }

  getUserOrders(): Observable<Order[]> {
    return this._apiClientService.get(`${this._config.ApiUrl}/ClientApi/UserOrders`);
  }

  addDishRate(dishRate: DishRate): void {
    this._apiClientService.post(`${this._config.ApiUrl}/ClientApi/RateDish`, { queryParams: dishRate });
  }

  addDelivererRate(delivererRate: DelivererRate): void {
    this._apiClientService.post(`${this._config.ApiUrl}/ClientApi/RateDeliverer`, { queryParams: delivererRate });
  }

}

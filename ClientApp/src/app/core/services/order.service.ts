import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DelivererRate, DishRate, UserOrder, WsOrder } from "src/app/shared/models/order.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";


@Injectable({
  providedIn: "root"
})
export class OrderService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  submitOrder(order: UserOrder): Observable<void> {
    return this._apiClientService.post(`${this._config.ApiUrl}/api/Orders/SubmitOrder`, { queryParams: order });
  }

  getUserOrders(): Observable<WsOrder[]> {
    return this._apiClientService.get(`${this._config.ApiUrl}/api/Orders/UserOrders`);
  }

  addDishRate(dishRate: DishRate): Observable<void> {
    return this._apiClientService.post(`${this._config.ApiUrl}/ClientApi/RateDish`, { queryParams: dishRate });
  }

  addDelivererRate(delivererRate: DelivererRate): Observable<void> {
    return this._apiClientService.post(`${this._config.ApiUrl}/ClientApi/RateDeliverer`, { queryParams: delivererRate });
  }

}

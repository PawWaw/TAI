import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { OrderStation } from "src/app/shared/models/restaurant.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";


@Injectable({
  providedIn: "root"
})
export class DishService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  // getDish(id: number): Observable<Dish> {
  //   return this._apiClientService.get(`${this._config.ApiUrl}/ClientApi/Dishes/Dish?Id=${id}`);
  // }

  getDishes(): Observable<OrderStation[]> {
    return this._apiClientService.get(`${this._config.ApiUrl}/ClientApi/DishesFromOrderStations`);
  }

  getDishesByRestaurant(id: number): Observable<OrderStation> {
    return this._apiClientService.get(`${this._config.ApiUrl}/ClientApi/DishesFromOrderStation?OrderStationId=${id}`);
  }


}

// todo: addDishRate, getUserOrders

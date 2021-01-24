import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { WsOrderStation } from "src/app/shared/models/restaurant.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";


@Injectable({
  providedIn: "root"
})
export class DishService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  getDishes(): Observable<WsOrderStation[]> {
    return this._apiClientService.get(`${this._config.ApiUrl}/api/OrderStations/DishFromOrderStations`);
  }

  getDishesByRestaurant(id: number): Observable<WsOrderStation> {
    return this._apiClientService.get(`${this._config.ApiUrl}/api/OrderStations/DishesFromOrderStation?OrderStationId=${id}`);
  }

}

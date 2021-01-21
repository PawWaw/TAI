import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { OrderStation } from "src/app/shared/models/restaurant.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";


@Injectable({
  providedIn: "root"
})
export class RestaurantService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  getRestaurants(): Observable<OrderStation[]> {
    return this._apiClientService.get(`${this._config.ApiUrl}/ClientApi/OrderStations`);
  }
}

import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { RestaurantService } from "src/app/core/services/restaurant.service";
import { WsOrderStation } from "src/app/shared/models/restaurant.interface";

@Injectable({
  providedIn: "root"
})
export class RestaurantViewService {

  constructor(private _restaurantServce: RestaurantService) { }

  getRestaurants(): Observable<WsOrderStation[]> {
    return this._restaurantServce.getRestaurants();
  }

  // getRestaurantsMock(): Observable<WsOrderStation[]> {
  //   return this.createData();
  // }

  // createData(): Observable<WsOrderStation[]> {
  //   let test: WsOrderStation[] = [
  //     {
  //       "id": 0,
  //       "city": "Ruda śląska",
  //       "address": "1 Maja 76",
  //       "wsRestaurant": {
  //         "id": 0,
  //         "name": "Wiśniowy Sad",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 1,
  //       "city": "Ruda Śląska",
  //       "address": "Teatralna 6b",
  //       "wsRestaurant": {
  //         "id": 1,
  //         "name": "Esencja Smaku",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 2,
  //       "city": "Katowice",
  //       "address": "Ligonia 16",
  //       "wsRestaurant": {
  //         "id": 2,
  //         "name": "Żurownia",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 3,
  //       "city": "Ruda Śląska",
  //       "address": "Wyzwolenia 22",
  //       "wsRestaurant": {
  //         "id": 3,
  //         "name": "Restauracja Barbórka",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 4,
  //       "city": "Katowice",
  //       "address": "Staromiejska 5",

  //       "wsRestaurant": {
  //         "id": 4,
  //         "name": "Tatiana",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 5,
  //       "city": "Katowice",
  //       "address": "Gliwicka 51",
  //       "wsRestaurant": {
  //         "id": 5,
  //         "name": "Fantasmagoria",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 6,
  //       "city": "Katowice",
  //       "address": "Wojewódzka 12",
  //       "wsRestaurant": {
  //         "id": 6,
  //         "name": "SmaQ Food & Wine",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 7,
  //       "city": "Katowice",
  //       "address": "al. Korfantego 35",
  //       "wsRestaurant": {
  //         "id": 7,
  //         "name": "Restauracja Śląska",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 8,
  //       "city": "Ruda Śląska",
  //       "address": "1 Maja 317",
  //       "wsRestaurant":
  //       {
  //         "id": 8,
  //         "name": "McDonald's",
  //         "wsDishWithRates": []
  //       }
  //     },
  //     {
  //       "id": 8,
  //       "city": "Ruda Śląska",
  //       "address": "Zabrzańska 49",
  //       "wsRestaurant":
  //       {
  //         "id": 8,
  //         "name": "McDonald's",
  //         "wsDishWithRates": []
  //       }
  //     }
  //   ]

  //   return of(test);
  // }
}

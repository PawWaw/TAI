import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { RestaurantService } from "src/app/core/services/restaurant.service";
import { OrderStation } from "src/app/shared/models/restaurant.interface";

@Injectable({
  providedIn: "root"
})
export class RestaurantViewService {

  constructor(private _restaurantServce: RestaurantService) { }

  getRestaurants(): Observable<OrderStation[]> {
    return this._restaurantServce.getRestaurants();
  }

  getRestaurantsMock(): Observable<OrderStation[]> {
    return this.createData();
  }

  createData(): Observable<OrderStation[]> {
    let test: OrderStation[] = [
      {
        "id": 0,
        "city": "Ruda śląska",
        "address": "1 Maja 76",
        "restaurant": {
          "id": 0,
          "name": "Wiśniowy Sad",
          "food": []
        }
      },
      {
        "id": 1,
        "city": "Ruda Śląska",
        "address": "Teatralna 6b",
        "restaurant": {
          "id": 1,
          "name": "Esencja Smaku",
          "food": []
        }
      },
      {
        "id": 2,
        "city": "Katowice",
        "address": "Ligonia 16",
        "restaurant": {
          "id": 2,
          "name": "Żurownia",
          "food": []
        }
      },
      {
        "id": 3,
        "city": "Ruda Śląska",
        "address": "Wyzwolenia 22",
        "restaurant": {
          "id": 3,
          "name": "Restauracja Barbórka",
          "food": []
        }
      },
      {
        "id": 4,
        "city": "Katowice",
        "address": "Staromiejska 5",

        "restaurant": {
          "id": 4,
          "name": "Tatiana",
          "food": []
        }
      },
      {
        "id": 5,
        "city": "Katowice",
        "address": "Gliwicka 51",
        "restaurant": {
          "id": 5,
          "name": "Fantasmagoria",
          "food": []
        }
      },
      {
        "id": 6,
        "city": "Katowice",
        "address": "Wojewódzka 12",
        "restaurant": {
          "id": 6,
          "name": "SmaQ Food & Wine",
          "food": []
        }
      },
      {
        "id": 7,
        "city": "Katowice",
        "address": "al. Korfantego 35",
        "restaurant": {
          "id": 7,
          "name": "Restauracja Śląska",
          "food": []
        }
      },
      {
        "id": 8,
        "city": "Ruda Śląska",
        "address": "1 Maja 317",
        "restaurant":
        {
          "id": 8,
          "name": "McDonald's",
          "food": []
        }
      },
      {
        "id": 8,
        "city": "Ruda Śląska",
        "address": "Zabrzańska 49",
        "restaurant":
        {
          "id": 8,
          "name": "McDonald's",
          "food": []
        }
      }
    ]

    return of(test);
  }
}

import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { DishService } from "src/app/core/services/dish.service";
import { OrderStation } from "src/app/shared/models/restaurant.interface";

@Injectable({
  providedIn: "root"
})
export class DishViewService {

  constructor(private _dishService: DishService) { }

  getDishes(): Observable<OrderStation[]> {
    return this._dishService.getDishes();
    // const dwr = this._dishService.getDishes().pipe(map((dishes: Dish[]): DishWithRate[] => {

    //   let dishesWithRate: DishWithRate[];
    //   let r = 0;

    //   dishes.forEach((elem) => {
    //     let dishWithRate: DishWithRate = {
    //       id: elem.id,
    //       name: elem.name,
    //       price: elem.price,
    //       ingredients: elem.ingredients,
    //       rate: 0
    //     }

    //     elem.dishRate.forEach((rt) => {
    //       r += rt.value;
    //     });

    //     if (r > 0) {
    //       r = r / elem.dishRate.length;
    //     }

    //     dishesWithRate.push(dishWithRate);
    //   });

    //   return dishesWithRate;
    // }));

    // return dwr;
  }

  getDishesByRestaurant(id: number): Observable<OrderStation> {
    return this._dishService.getDishesByRestaurant(id);




    // const dishesByRestaurant = this._dishService.getDishesByRestaurant(id).pipe(map((dishes: Dish[]): DishWithRate[] => {

    //   let dishesWithRate: DishWithRate[];
    //   let r = 0;

    //   dishes.forEach((elem) => {
    //     let dishWithRate: DishWithRate = {
    //       id: elem.id,
    //       name: elem.name,
    //       price: elem.price,
    //       ingredients: elem.ingredients,
    //       rate: 0
    //     }

    //     elem.dishRate.forEach((rt) => {
    //       r += rt.value;
    //     });

    //     if (r > 0) {
    //       r = r / elem.dishRate.length;
    //     }

    //     dishesWithRate.push(dishWithRate);
    //   });

    //   return dishesWithRate;
    // }));

    // return dishesByRestaurant;
  }

  getDishesMock(): Observable<OrderStation[]> {
    return this.createData();
  }

  getDishesByRestaurantMock(id: number): Observable<OrderStation> {
    return this.createDataByRestaurant();
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
          "food": [
            {
              "id": 3,
              "name": "pizza margaritta",
              "price": 22,
              "ingredients": ["ser", "pomidor"],
              "rate": 4.2
            },
            {
              "id": 4,
              "name": "pizza hawajska",
              "price": 26,
              "ingredients": ["ser", "pomidor", "ananas", "szynka"],
              "rate": 4.5
            }
          ]
        }
      },
      {
        "id": 1,
        "city": "Ruda Śląska",
        "address": "Teatralna 6b",
        "restaurant": {
          "id": 1,
          "name": "Esencja Smaku",
          "food": [
            {
              "id": 0,
              "name": "rosół z kury",
              "price": 10,
              "ingredients": ["kurczak", "makaron", "pietruszka"],
              "rate": 4.9
            },
            {
              "id": 1,
              "name": "spaghetti",
              "price": 15,
              "ingredients": ["makaron", "mieso wieprzowe", "pomidor"],
              "rate": 4.1
            },
            {
              "id": 2,
              "name": "pomidorowa",
              "price": 12,
              "ingredients": ["kurczak", "makaron", "pomidor"],
              "rate": 4.0
            },
          ]
        }
      },
      {
        "id": 2,
        "city": "Katowice",
        "address": "Ligonia 16",
        "restaurant": {
          "id": 2,
          "name": "Żurownia",
          "food": [
            {
              "id": 5,
              "name": "kebab w ciescie",
              "price": 16,
              "ingredients": ["mieso jagniece", "kapuska", "marchewka", "sos"],
              "rate": 3.7
            },
            {
              "id": 6,
              "name": "rolada wolowa z kluskami slaskimi i modra kapusta",
              "price": 30,
              "ingredients": ["mieso wolowe", "boczek", "musztarda", "kapusta czerwona", "kluski"],
              "rate": 3.6
            },
            {
              "id": 7,
              "name": "kotlet chabowy z frytkami i surowka",
              "price": 10,
              "ingredients": ["shab wieprzowy", "frytki", "kapusta kiszona", "marchewka"],
              "rate": 4
            }
          ]
        }
      }
    ]

    return of(test);
  }

  createDataByRestaurant(): Observable<OrderStation> {
    let test: OrderStation = {
      "id": 0,
      "city": "Ruda śląska",
      "address": "1 Maja 76",
      "restaurant": {
        "id": 0,
        "name": "Wiśniowy Sad",
        "food": [
          {
            "id": 1,
            "name": "spaghetti",
            "price": 15,
            "ingredients": ["makaron", "mieso wieprzowe", "pomidor"],
            "rate": 4.1
          },
          {
            "id": 3,
            "name": "pizza margaritta",
            "price": 22,
            "ingredients": ["ser", "pomidor"],
            "rate": 4.2
          },
          {
            "id": 4,
            "name": "pizza hawajska",
            "price": 26,
            "ingredients": ["ser", "pomidor", "ananas", "szynka"],
            "rate": 4.5
          }
        ]
      }
    }

    return of(test);
  }
}


import { Food } from "./dish.interface";
import { OrderStation } from "./restaurant.interface";

export class UserOrder {
  dishes: DishOrder[];

  constructor() {
    this.dishes = [];
  }
}

export interface DishOrder {
  orderStationId: number;
  food: FoodOrdered[];
}

export interface FoodOrdered {
  dishId: number;
  count: number;
}

export interface Order {
  id: number;
  delivererId: number;
  orderStation: OrderStation;
  status: string;
  startTime: Date;
  endTime: Date;
  food: Food[];
}

export interface DelivererRate {
  id: number;
  rate: number;
}

export interface DishRate {
  id: number;
  rate: number;
}

export interface RatePick {
  value: number;
  typeName: string;
}

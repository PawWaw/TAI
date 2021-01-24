import { Food as WsFood } from "./dish.interface";
import { WsOrderStation } from "./restaurant.interface";

export class UserOrder {
  dishes: DishOrder[];

  constructor() {
    this.dishes = [];
  }
}

export interface DishOrder {
  orderStationId: number;
  foods: FoodOrdered[];
}

export interface FoodOrdered {
  dishId: number;
  count: number;
}

export interface WsOrder {
  id: number;
  delivererId: number;
  wsOrderStation: WsOrderStation;
  status: string;
  startTime: string;
  endTime: string;
  wsFood: WsFood[];
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

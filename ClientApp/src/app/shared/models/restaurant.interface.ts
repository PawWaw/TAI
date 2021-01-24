import { DishWithRate as WsDishWithRates } from "./dish.interface";

export interface WsRestaurant {
  id: number;
  name: string;
  wsDishWithRates: WsDishWithRates[];
}

export interface WsOrderStation {
  id: number;
  city: string;
  address: string;
  wsRestaurant: WsRestaurant;
}

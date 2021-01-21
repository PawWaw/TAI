import { DishWithRate } from "./dish.interface";

export interface Restaurant {
  id: number;
  name: string;
  food: DishWithRate[];
}

export interface OrderStation {
  id: number;
  city: string;
  address: string;
  restaurant: Restaurant;
}

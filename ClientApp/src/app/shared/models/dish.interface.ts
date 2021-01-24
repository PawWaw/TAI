export interface Dish {
  id: number;
  orderStationId: number;
  restaurantId: number;
  name: string;
  price: number;
  ingredients: String[];
  restaurantName: string;
  city: string;
  address: string;
  rate: number;
}

export interface DishesInBasket {
  dish: Dish;
  count: number;
}

export interface DishWithRate {
  id: number;
  name: string;
  price: number;
  ingredients: String[];
  rate: number;
}

export interface Food {
  id: number;
  name: string;
  price: number;
}

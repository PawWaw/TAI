export interface DishRate {
  id: number;
  userId: number;
  foodId: number;
  value: number;
}

export interface FoodRate {
  id: number;
  userId: number;
  date: Date;
  value: number;
}

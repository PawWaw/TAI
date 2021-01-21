import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { Dish, DishesInBasket } from "src/app/shared/models/dish.interface";
import { User } from "src/app/shared/models/user.interface";

@Injectable({
  providedIn: "root"
})
export class ModelService {
  private _user: User = null;
  private _dishesBasket: DishesInBasket[] = [];
  private _elementsInBasket = new Subject<string>();
  private _loggedinSource = new Subject<boolean>();
  private _userLoggedin: boolean;

  loggedin$ = this._loggedinSource.asObservable();
  elementsInBasket$ = this._elementsInBasket.asObservable();

  setStatus(status: boolean): void {
    this._loggedinSource.next(status);
    this._userLoggedin = status;
  }

  updateElementIntoBasket(): void {
    let elemInBasket = 0;
    this._dishesBasket.forEach((x) => {
      elemInBasket += x.count;
    });

    let s: string = "" + elemInBasket;
    if (elemInBasket === 0) {
      s = "";
    }
    this._elementsInBasket.next(s);
  }

  getUserLoggedin(): boolean {
    return this._userLoggedin;
  }

  getOrderedDishes(): DishesInBasket[] {
    return this._dishesBasket;
  }

  addDishToOrder(dish: Dish): void {
    if (this._dishesBasket.length === 0) {
      let d: DishesInBasket = {
        dish: dish,
        count: 1
      }
      this._dishesBasket.push(d);
    }
    else {
      let dishes = this._dishesBasket.filter(x => x.dish.id === dish.id);
      if (dishes.length === 0) {
        this._dishesBasket.push({ dish: dish, count: 1 });
      }
      else {
        let index = this._dishesBasket.indexOf(dishes[0]);
        this._dishesBasket[index].count++;
      }
    }

    this.updateElementIntoBasket();
  }

  simpleAddOneDishToOrder(dishesInBasket: DishesInBasket): DishesInBasket[] {
    let dishes = this._dishesBasket.filter(x => x.dish.id === dishesInBasket.dish.id);
    let index = this._dishesBasket.indexOf(dishes[0]);
    this._dishesBasket[index].count++;
    this.updateElementIntoBasket();
    return this._dishesBasket;
  }

  simpleRemoveOneDishFromOrder(dishesInBasket: DishesInBasket): DishesInBasket[] {
    let dishes = this._dishesBasket.filter(x => x.dish.id === dishesInBasket.dish.id);
    let index = this._dishesBasket.indexOf(dishes[0]);
    if (this._dishesBasket[index].count > 1) {
      this._dishesBasket[index].count--;
    }
    this.updateElementIntoBasket();
    return this._dishesBasket;
  }

  deleteDish(dishInBasket: DishesInBasket): DishesInBasket[] {
    let dishes = this._dishesBasket.filter(x => x.dish.id === dishInBasket.dish.id);
    let index = this._dishesBasket.indexOf(dishes[0]);
    this._dishesBasket.splice(index, 1);
    this.updateElementIntoBasket();
    return this._dishesBasket;
  }

  deleteAllDishesInBasket(): void {
    this._dishesBasket = [];
    this.updateElementIntoBasket();
  }

  clearData(): void {
    this._user = null
    window.localStorage.setItem("jwtToken", "");

    this._dishesBasket = [];
    this.setStatus(false);
  }

  setUser(user: User): void {
    this._user = user;
  }

  // start user getter
  getUser(): User {
    return this._user;
  }

  getUserId(): number {
    if (this._user !== null) {
      return this._user.id;
    }
    return -1;
  }

  getUserUsername(): string {
    if (this._user !== null) {
      return this._user.username;
    }
    return "";
  }

  getUserCity(): string {
    if (this._user !== null) {
      return this._user.city;
    }
    return "";
  }

  getUserAddress(): string {
    if (this._user !== null) {
      return this._user.address;
    }
    return "";
  }

  getUserFirstName(): string {
    if (this._user !== null) {
      return this._user.firstName;
    }
    return "";
  }

  getUserLastName(): string {
    if (this._user !== null) {
      return this._user.lastName;
    }
    return "";
  }

  getUserEmail(): string {
    if (this._user !== null) {
      return this._user.email;
    }
    return "";
  }
  // end user getter
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, retry } from "rxjs/operators";
import { Dish } from '../_models/Dish';

@Injectable({
  providedIn: 'root'
})
export class DishService {
  Dishes: Dish[] = [
    {
      id: 1,
      name: 'spaghetti',
      price: 16.99,
      ingredients: [
        {
          id: 1,
          name: 'makaron pszenny'
        },
        {
          id: 2,
          name: 'sos pomidorowy'
        },
        {
          id: 3,
          name: 'mięso mielone'
        }
      ]
    },
    {
      id: 2,
      name: 'omlet',
      price: 12.99,
      ingredients: [
        {
          id: 4,
          name: 'ziemniaki'
        },
        {
          id: 6,
          name: 'jajko'
        }
      ]
    },
    {
      id: 3,
      name: 'Coca-Cola 0.5l',
      price: 3.99,
      ingredients: []
    }
  ]

  dishUrl = 'http://localhost:8080/dish';
  temp: any;

  constructor(private http: HttpClient) { 
  }

  httpHeader = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public getDishes(): Dish[] {
  // public getDishes(): Observable<Dish[]> {
  //   return this.http.get<Dish[]>(this.dishUrl, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    return this.Dishes;
  }

  public getSingleDish(name: string): Dish {
  // public getSingleDish(name: string): Observable<Dish> {
  //   return this.http.get<Dish>(this.dishUrl + "/" + name, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    return this.Dishes.find(x => x.name == name);
  }

  public postDish(dish: Dish) {
    //this.http.post<Dish>(this.dishUrl, dish, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    this.Dishes.push(dish);
  }

  public modifyDish(dish: Dish) {
    this.http.patch(this.dishUrl, dish, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}

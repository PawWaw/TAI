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
      name: 'Spaghetti',
      price: 16.99,
      ingredients: [
          'Makaron pszenny', 'Sos pomidorowy', 'Mięso mielone']
    },
    {
      id: 2,
      name: 'Omlet',
      price: 12.99,
      ingredients: ['Ziemniaki','Jajko']
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

  public getSingleDish(id: number): Dish {
  // public getSingleDish(name: string): Observable<Dish> {
  //   return this.http.get<Dish>(this.dishUrl + "/" + id, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    return this.Dishes.find(x => x.id == id);
  }

  public postDish(dish: Dish) {
    //this.http.post<Dish>(this.dishUrl, dish, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    this.Dishes.push(dish);
  }

  public modifyDish(dish: Dish) {
    this.http.patch(this.dishUrl + "/" + dish.id, dish, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
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

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, retry } from "rxjs/operators";
import { Dish } from '../_models/Dish';

@Injectable({
  providedIn: 'root'
})
export class DishService {

  dishUrl = 'https://localhost:44308/api/foods';
  temp: any;

  constructor(private http: HttpClient) { 
  }

  httpHeader = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public getDishes(): Observable<Dish[]> {
    return this.http.get<Dish[]>(this.dishUrl, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  public getSingleDish(id: number): Observable<Dish> {
    return this.http.get<Dish>(this.dishUrl + "/" + id, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  public postDish(dish: Dish) {
    return this.http.post<Dish>(this.dishUrl, dish, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  public modifyDish(dish: Dish) {
    return this.http.patch(this.dishUrl + "/" + dish.id, dish, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  public deleteDish(id: number) {
    return this.http.delete(this.dishUrl + "/" + id, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
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

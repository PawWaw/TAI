import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Deliverer } from '../_models/Deliverer';
import { Order } from '../_models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  orderUrl = 'https://localhost:44308/api/orders';
  delivererUrl = 'https://localhost:44308/api/deliverers'
  temp: any;

  constructor(private http: HttpClient) { 
  }

  httpHeader = new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('auth_token'),
    'Content-Type': 'application/json'
  });

    public getDeliverers(): Observable<Deliverer[]> {
    return this.http.get<Deliverer[]>(this.delivererUrl, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  public getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.orderUrl + "/isActive?current=true", {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  // public getSingleOrder(code: string): Observable<Order> {
  //   return this.http.get<Order>(this.orderUrl + "/" + code, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  // }

  // public postOrder(order: Order) {
  //   this.http.post<Order>(this.orderUrl, order, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  // }

  public modifyOrder(order: Order) {
    order.orderStation = null
    return this.http.patch(this.orderUrl + "/" + order.id, order, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
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

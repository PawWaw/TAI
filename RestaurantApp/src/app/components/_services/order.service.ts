import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Order } from '../_models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  Deliverers: String[] = ['Dzidosław Żuberek', 'Andrzej Andrzejczak', 'Susan Wójcicki']

  Orders: Order[] = [
    {
      id: 1,
      orderStationId: 1,
      delivererId: 2,
      userId: 3,
      status: "pending",
      startTime: "00:00:01",
      endTime: "-",
      deliverer: "Dzidosław Żuberek",
      orderStation: "Katowice"
    },
    {
      id: 2,
      orderStationId: 1,
      delivererId: 2,
      userId: 4,
      status: "pending",
      startTime: "03:24:01",
      endTime: "-",
      deliverer: "Dzidosław Żuberek",
      orderStation: "Katowice"
    },
    {
      id: 3,
      orderStationId: 2,
      delivererId: 3,
      userId: 3,
      status: "waiting",
      startTime: "16:50:00",
      endTime: "-",
      deliverer: "",
      orderStation: "Sosnowiec"
    }
  ]

  orderUrl = 'http://localhost:8080/order';
  delivererUrl = 'http://localhost:8080/deliverer'
  temp: any;

  constructor(private http: HttpClient) { 
  }

  httpHeader = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public getDeliverers(): String[] {
    // public getDeliverers(): Observable<String[]> {
    //return this.http.get<Order[]>(this.delivererUrl, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    return this.Deliverers;
  }

  public getOrders(): Order[] {
  // public getOrders(): Observable<Order[]> {
    //return this.http.get<Order[]>(this.orderUrl, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    return this.Orders;
  }

  public getSingleOrder(id: number): Order {
  // public getSingleOrder(code: string): Observable<Order> {
    //return this.http.get<Order>(this.orderUrl + "/" + code, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
    return this.Orders.find(x => x.id == id);
  }

  public postOrder(order: Order) {
    this.http.post<Order>(this.orderUrl, order, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  public modifyOrder(order: Order) {
    this.http.patch(this.orderUrl, order, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
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

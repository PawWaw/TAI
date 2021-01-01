import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../_models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderHistoryService {

  Orders: Order[] = [
    {
      id: 1,
      orderStationId: 1,
      delivererId: 2,
      userId: 3,
      status: "delivered",
      startTime: "00:00:01",
      endTime: "01:02:03",
      deliverer: "Dzidosław Żuberek",
      orderStation: "Katowice"
    },
    {
      id: 2,
      orderStationId: 1,
      delivererId: 2,
      userId: 4,
      status: "delivered",
      startTime: "03:24:01",
      endTime: "11:42:03",
      deliverer: "Dzidosław Żuberek",
      orderStation: "Katowice"
    },
    {
      id: 3,
      orderStationId: 2,
      delivererId: 3,
      userId: 3,
      status: "delivered",
      startTime: "16:50:00",
      endTime: "18:12:00",
      deliverer: "Andrzej Andrzejewski",
      orderStation: "Sosnowiec"
    }
  ]
  
  orderUrl = 'http://localhost:8080/orderHistory';
  temp: any;

  constructor(private http: HttpClient) { 
  }

  httpHeader = new HttpHeaders({
    'Content-Type': 'application/json'
  });

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
}

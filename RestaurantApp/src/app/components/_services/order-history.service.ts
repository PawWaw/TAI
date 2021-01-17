import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, retry } from 'rxjs/operators';
import { Order } from '../_models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderHistoryService {
  
  orderUrl = 'https://localhost:44308/api/orders';
  temp: any;

  constructor(private http: HttpClient) { 
  }

  httpHeader = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  public getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.orderUrl + "/" + localStorage.getItem('username') + "/isCurrent?current=false", {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
  }

  public getSingleOrder(code: string): Observable<Order> {
    return this.http.get<Order>(this.orderUrl + "/" + code, {headers: this.httpHeader}).pipe(retry(1), catchError(this.errorHandler));
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

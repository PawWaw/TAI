import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams, HttpResponse} from "@angular/common/http";
import * as moment from "moment";
import {catchError, map, retry} from "rxjs/operators";
import {Observable, throwError} from "rxjs";
import { AuthResponse } from '../_models/AuthResponse';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseurl = 'https://localhost:44308/api/owners/auth'; 
  constructor(private http: HttpClient) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  authenticate(value: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.baseurl, value).pipe(retry(1), catchError(this.errorHandler));
  }

  // saveSession(data: HttpResponse<any>){
  //   const expiresAt = moment().add(data.headers.get("Expiration"));
  //   console.log(data.headers.get("token"))
  //   localStorage.setItem('auth_token',data.headers.get("token"));
  //   localStorage.setItem('token_exp_time', JSON.stringify(expiresAt.valueOf()));
  //   localStorage.setItem('current_user',data.headers.get("Username"));
  //   if (data.headers.get("Household")!=null) {
  //     localStorage.setItem('current_household', data.headers.get("Household"));
  //   }
  // }

  logout(){
    localStorage.removeItem('auth_token');
    localStorage.removeItem('token_exp_time');
    localStorage.removeItem('current_user');
    localStorage.removeItem('current_household');
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem("token_exp_time");
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }
  errorHandler(error) {
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

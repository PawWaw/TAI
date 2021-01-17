import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams, HttpResponse} from "@angular/common/http";
import * as moment from "moment";
import {catchError, map} from "rxjs/operators";
import {throwError} from "rxjs";


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseurl = 'https://localhost:44308/api/users/auth'; 
  constructor(private http: HttpClient) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  authenticate(value: string){
    return this.http.post(this.baseurl,value, {responseType: 'text'}).pipe(map(token => {
      localStorage.setItem('auth_token', token);
      return token;
  }), catchError(this.errorHandler));
  }

  saveSession(data: HttpResponse<any>){
    const expiresAt = moment().add(data.headers.get("Expiration"));
    localStorage.setItem('auth_token',data.headers.get("Authorization"));
    localStorage.setItem('token_exp_time', JSON.stringify(expiresAt.valueOf()));
    localStorage.setItem('current_user',data.headers.get("Username"));
    if (data.headers.get("Household")!=null) {
      localStorage.setItem('current_household', data.headers.get("Household"));
    }
  }

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

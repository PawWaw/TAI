import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

interface DataParam { [param: string]: any; }

interface QueryParameters {
  queryParams?: DataParam;
}

@Injectable({
  providedIn: "root"
})
export class ApiClientService {

  constructor(private readonly _http: HttpClient) { }

  get<T>(url: string): Observable<T> {
    const header = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("jwtToken")}`);
    return this._http.get<T>(url, { headers: header });
  }

  post<T>(
    url: string,
    queryParameters?: QueryParameters): Observable<T> {
    const header = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("jwtToken")}`);
    return this._http.post<T>(url, queryParameters?.queryParams, { headers: header });
  }

  put<T>(
    url: string,
    queryParameters?: QueryParameters): Observable<T> {
    const header = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("jwtToken")}`);
    return this._http.put<T>(url, queryParameters?.queryParams, { headers: header });
  }

  delete<T>(url: string): Observable<T> {
    const header = new HttpHeaders().set("Authorization", `Bearer ${localStorage.getItem("jwtToken")}`);
    return this._http.delete<T>(url, { headers: header });
  }
}

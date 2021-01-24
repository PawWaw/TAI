import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from "src/app/shared/models/user.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";


@Injectable({
  providedIn: "root"
})
export class UserService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  updateUser(user: User): Observable<void> {
    return this._apiClientService.put(`${this._config.ApiUrl}/api/Users/user`, { queryParams: user });
  }
}

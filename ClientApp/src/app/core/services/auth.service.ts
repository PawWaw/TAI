import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiToken } from "src/app/shared/models/api-token.interface";
import { UserAuth } from "src/app/shared/models/user-auth.interface";
import { User } from "src/app/shared/models/user.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";

@Injectable({
  providedIn: "root"
})
export class AuthService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  loginUser(auth: UserAuth): Observable<ApiToken> {
    return this._apiClientService.post(`${this._config.ApiUrl}/ClientApi/Login`, { queryParams: auth });
  }

  registerUser(user: User){
    return this._apiClientService.post(`${this._config.ApiUrl}/ClientApi/RegisterUser`, { queryParams: user });
  }
}

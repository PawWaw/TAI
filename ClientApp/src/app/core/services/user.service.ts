import { Injectable } from "@angular/core";
import { User } from "src/app/shared/models/user.interface";

import { ApiClientService } from "./api-client.service";
import { ConfigService } from "./config.service";


@Injectable({
  providedIn: "root"
})
export class UserService {

  constructor(private _apiClientService: ApiClientService, private _config: ConfigService) { }

  updateUser(user: User): void {
    this._apiClientService.put(`${this._config.ApiUrl}/ClientApi/User`, { queryParams: user });
  }
}

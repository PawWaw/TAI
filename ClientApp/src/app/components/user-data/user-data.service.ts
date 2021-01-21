import { Injectable } from "@angular/core";
import { UserService } from "src/app/core/services/user.service";
import { User } from "src/app/shared/models/user.interface";

@Injectable({
  providedIn: "root"
})
export class UserDataService {

  constructor(private _userService: UserService) { }

  updateUser(user: User): void {
    this._userService.updateUser(user);
  }
}

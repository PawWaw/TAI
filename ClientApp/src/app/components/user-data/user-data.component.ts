import { Component, Inject } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { ModelService } from "src/app/core/services/model.service";
import { User } from "src/app/shared/models/user.interface";

import { UserDataService } from "./user-data.service";

@Component({
  selector: 'app-user-data',
  templateUrl: './user-data.component.html',
  styleUrls: ['./user-data.component.scss']
})
export class UserDataComponent {

  userDataForm = this._fb.group({
    id: [null],
    username: [null],
    email: [null],
    firstName: [null],
    lastName: [null],
    city: [null],
    address: [null]
  });

  constructor(
    private _fb: FormBuilder,
    private _userDataService: UserDataService,
    private _modelService: ModelService,
    public dialogRef: MatDialogRef<UserDataComponent>,
    @Inject(MAT_DIALOG_DATA) public data: User) {
    this.userDataForm.patchValue({
      id: data.id,
      username: data.username,
      email: data.email,
      firstName: data.firstName,
      lastName: data.lastName,
      city: data.city,
      address: data.address
    });
  }

  updateUser(): void {
    console.log("aktualizacja");

    const user: User = {
      id: this.userDataForm.value.id,
      username: this.userDataForm.value.username,
      email: this.userDataForm.value.email,
      firstName: this.userDataForm.value.firstName,
      lastName: this.userDataForm.value.lastName,
      city: this.userDataForm.value.city,
      address: this.userDataForm.value.address
    }

    // this._userDataService.updateUser(user);
    this._modelService.setUser(user);
  }
}

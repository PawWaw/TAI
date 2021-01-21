import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "src/app/core/services/auth.service";
import { ModelService } from "src/app/core/services/model.service";
import { UserAuth } from "src/app/shared/models/user-auth.interface";
import { User } from "src/app/shared/models/user.interface";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent implements OnInit {

  loading = false;
  error = false;
  formGroup: FormGroup;

  constructor(private _formBuilder: FormBuilder,
    private _authService: AuthService,
    private _router: Router,
    private _modelService: ModelService) {
  }

  ngOnInit() {
    this.formGroup = this._formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  get username() { return this.formGroup.get('username'); }
  get password() { return this.formGroup.get('password'); }

  onSubmit() {
    this.loading = true;

    const auth: UserAuth = {
      username: this.formGroup.value.username,
      password: this.formGroup.value.password
    };

    // to jest zrówrocony user -> potem usunąć
    const user: User = {
      id: 0,
      username: "Adam",
      email: "aa@aa.aa",
      firstName: "Adam",
      lastName: "Adam",
      city: "Katowice",
      address: "Sokolska 123"
    }

    this._modelService.setUser(user);
    this._modelService.setStatus(true);
    this._router.navigate([""]);

    // this._authService.loginUser(auth).subscribe((x: ApiToken) => {
    //   window.localStorage.setItem("jwtToken", x.accessToken);
    //   this._modelService.setUserParams(x.user.id, x.user.username);
    //   this._router.navigate([""]);
    //   this.formGroup.controls.password.setValue(null);
    // },
    //   error => {
    //     this.error = true;
    //     this.loading = false;
    //   });
  }

}

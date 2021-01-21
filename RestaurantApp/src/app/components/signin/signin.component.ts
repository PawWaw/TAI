import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { first } from 'rxjs/internal/operators/first';
import { DataSharingService } from '../_services/data-sharing.service';
import { AuthResponse } from '../_models/AuthResponse';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

  loading=false;
  error=false;
  formGroup: FormGroup;
  token: AuthResponse;

    constructor(private formBuilder: FormBuilder,
                private authService: AuthService,
                private dataSharingService: DataSharingService,
                private router: Router) {
  }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  get username() {return this.formGroup.get('username');}
  get password() { return this.formGroup.get('password'); }

  onSubmit(){
    this.loading=true;

    this.formGroup.patchValue({username: this.username.value.toString().toLowerCase()});

    this.authService.authenticate(this.formGroup.value).subscribe(
      data=>{
        this.token = data
        localStorage.setItem('auth_token', this.token.token);
        console.log(localStorage.getItem('auth_token'))
        this.router.navigate(['/history']);
        this.dataSharingService.isLogged.next(true);
      },
      error=>{
        this.error=true;
        this.loading=false;
      }
    );
  }

}

import {Component, OnInit} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormGroupDirective,
  NgForm,
  ValidationErrors,
  ValidatorFn,
  Validators
} from "@angular/forms";
import {ErrorStateMatcher} from "@angular/material/core";
import {first} from "rxjs/operators";
import {Router} from "@angular/router";
import { UserService } from '../_services/user.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

export const passwordMatchValidator: ValidatorFn = (formGroup: FormGroup): ValidationErrors | null => {
  if (formGroup.get('password').value === formGroup.get('password2').value)
    return null;
  else
    return {passwordMismatch: true};
};

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  loading = false;
  minPwLength = 5;
  formGroup: FormGroup;
  errorText = null;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router) {
  }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(this.minPwLength)]],
      email: ['', [Validators.email, Validators.required]],
      password: ['', [Validators.required, Validators.minLength(this.minPwLength)]],
      password2: ['', [Validators.required]],
      firstName: '',
      lastName: '',
      city: '',
      address: '',
      restaurant: ''
    }, {validator: passwordMatchValidator});
  }

  get username() {
    return this.formGroup.get('username');
  }

  get email() {
    return this.formGroup.get('email');
  }

  get password() {
    return this.formGroup.get('password');
  }

  get password2() {
    return this.formGroup.get('password2');
  }
  
  get city() {
    return this.formGroup.get('city');
  }

  get address() {
    return this.formGroup.get('address');
  }

  get restaurant() {
    return this.formGroup.get('restaurant');
  }

  onPasswordInput() {
    if (this.formGroup.hasError('passwordMismatch'))
      this.password2.setErrors([{'passwordMismatch': true}]);
    else
      this.password2.setErrors(null);
  }

  onSubmit() {
    if (this.formGroup.invalid) {
      return;
    }
    this.loading = true;

    this.formGroup.patchValue({username: this.username.value.toString().toLowerCase()});

    this.userService.postUser(this.formGroup.value).pipe(first()).subscribe(
      data => {
        console.log(this.formGroup.value)
        this.router.navigate(['/signin']);
      },
      error => {
        this.loading = false;
        this.errorText = "Username and/or email are already taken!"
      }
    )
  }

  matcher = new MyErrorStateMatcher();
}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { SigninComponent } from './components/signin/signin.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSliderModule } from '@angular/material/slider';
import { MatButtonModule} from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatListModule } from '@angular/material/list';
import { SignupComponent } from './components/signup/signup.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { OAuthModule } from 'angular-oauth2-oidc';
import { DishComponent } from './components/dish/dish.component';
import { OrderComponent } from './components/order/order.component';
import { OrderHistoryComponent } from './components/order-history/order-history.component';
import { OrderDetailsComponent } from './components/order-details/order-details.component';
import { DishDetailsComponent } from './components/dish-details/dish-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    SigninComponent,
    SignupComponent,
    DishComponent,
    OrderComponent,
    OrderHistoryComponent,
    OrderDetailsComponent,
    DishDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSliderModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    MatSelectModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatCardModule,
    MatSnackBarModule,
    MatListModule,
    MatProgressSpinnerModule,
    OAuthModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";

import { MaterialModule } from "../material.module";
import { BasketViewComponent } from "./basket-view/basket-view.component";
import { DishByRestaurantComponent } from "./dish-by-restaurant/dish-by-restaurant.component";
import { DishViewComponent } from "./dish-view/dish-view.component";
import { DishComponent } from "./dish/dish.component";
import { HeaderComponent } from "./header/header.component";
import { HomeComponent } from "./home/home.component";
import { OrderComponent } from "./order/order.component";
import { RateDelivererViewComponent } from "./rate-deliverer-view/rate-deliverer-view.component";
import { RateDishViewComponent } from "./rate-dish-view/rate-dish-view.component";
import { RestaurantComponent } from "./restaurant/restaurant.component";
import { SigninComponent } from "./signin/signin.component";
import { SignupComponent } from "./signup/signup.component";
import { UserDataComponent } from "./user-data/user-data.component";

@NgModule({
  declarations: [
    HeaderComponent,
    DishComponent,
    HomeComponent,
    SigninComponent,
    SignupComponent,
    DishViewComponent,
    RestaurantComponent,
    DishByRestaurantComponent,
    UserDataComponent,
    BasketViewComponent,
    OrderComponent,
    RateDishViewComponent,
    RateDelivererViewComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  exports: [
    HeaderComponent
  ]
})
export class ComponentsModule { }

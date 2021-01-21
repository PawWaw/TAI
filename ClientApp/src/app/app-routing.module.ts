import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { DishByRestaurantComponent } from "./components/dish-by-restaurant/dish-by-restaurant.component";
import { DishComponent } from "./components/dish/dish.component";
import { HomeComponent } from "./components/home/home.component";
import { OrderComponent } from "./components/order/order.component";
import { RestaurantComponent } from "./components/restaurant/restaurant.component";
import { SigninComponent } from "./components/signin/signin.component";
import { SignupComponent } from "./components/signup/signup.component";

const routes: Routes = [
  {
    path: '', component: HomeComponent, pathMatch: 'full'
  },
  {
    path: 'dishes', component: DishComponent, pathMatch: 'full'
  },
  {
    path: 'signup', component: SignupComponent, pathMatch: 'full'
  },
  {
    path: 'signin', component: SigninComponent, pathMatch: 'full'
  },
  {
    path: 'restaurants', component: RestaurantComponent, pathMatch: 'full'
  },
  {
    path: 'restaurant/:id', component: DishByRestaurantComponent, pathMatch: 'full'
  },
  {
    path: 'orders', component: OrderComponent, pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

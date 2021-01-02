import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderDetailsComponent } from './components/order-details/order-details.component';
import { DishComponent } from './components/dish/dish.component';
import { HomeComponent } from './components/home/home.component';
import { OrderHistoryComponent } from './components/order-history/order-history.component';
import { OrderComponent } from './components/order/order.component';
import { SigninComponent } from './components/signin/signin.component';
import { SignupComponent } from './components/signup/signup.component';
import { DishDetailsComponent } from './components/dish-details/dish-details.component';
import { DishModifyComponent } from './components/dish-modify/dish-modify.component';
import { AddDishComponent } from './components/add-dish/add-dish.component';

const routes: Routes = [
  {
    path: '', component: HomeComponent, pathMatch: 'full'
  },
  {
    path: 'dish/create', component: AddDishComponent, pathMatch: 'full'
  },
  {
    path: 'dish', component: DishComponent, pathMatch: 'full'
  },
  {
    path: 'dish/details', component: DishDetailsComponent, pathMatch: 'full'
  },
  {
    path: 'dish/modify', component: DishModifyComponent, pathMatch: 'full'
  },
  {
    path: 'signin', component: SigninComponent, pathMatch: 'full'
  },
  {
    path: 'order', component: OrderComponent, pathMatch: 'full'
  },
  {
    path: 'order/details', component: OrderDetailsComponent, pathMatch: 'full'
  },
  {
    path: 'history', component: OrderHistoryComponent, pathMatch: 'full'
  },
  {
    path: 'signup', component: SignupComponent, pathMatch: 'full'
  },
  { 
    path: '**', redirectTo: 'home' 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

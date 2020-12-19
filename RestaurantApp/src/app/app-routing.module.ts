import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DishComponent } from './components/dish/dish.component';
import { HomeComponent } from './components/home/home.component';
import { LocalComponent } from './components/local/local.component';
import { SigninComponent } from './components/signin/signin.component';
import { SignupComponent } from './components/signup/signup.component';

const routes: Routes = [
  {
    path: '', component: HomeComponent, pathMatch: 'full'
  },
  {
    path: 'dish', component: DishComponent, pathMatch: 'full'
  },
  {
    path: 'local', component: LocalComponent, pathMatch: 'full'
  },
  {
    path: 'signin', component: SigninComponent, pathMatch: 'full'
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

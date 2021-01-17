import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Dish } from '../_models/Dish';
import { Ingredient } from '../_models/Ingredient';

@Component({
  selector: 'app-dish-details',
  templateUrl: './dish-details.component.html',
  styleUrls: ['./dish-details.component.css']
})
export class DishDetailsComponent implements OnInit {

  formGroup: FormGroup;
  dish: Dish;
  ingredients: String[];

  constructor(
    private _location: Location,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit() {
    this.dish = JSON.parse(localStorage.getItem('row'));
    console.log(this.dish);
    this.ingredients = this.dish.ingredients;
  }

  backClicked() {
    this._location.back();
  }
}

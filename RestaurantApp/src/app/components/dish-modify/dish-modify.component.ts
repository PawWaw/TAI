import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Dish } from '../_models/Dish';
import { Ingredient } from '../_models/Ingredient';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-dish-modify',
  templateUrl: './dish-modify.component.html',
  styleUrls: ['./dish-modify.component.css']
})
export class DishModifyComponent implements OnInit {

  formGroup: FormGroup;
  dish: Dish;
  modifiedDish: Dish = new Dish;
  ingredients: String[];
  value = 'Clear me';

  constructor(
    private _location: Location,
    private _snackBar: MatSnackBar,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit() {
    this.dish = JSON.parse(localStorage.getItem('row'));
    this.ingredients = this.dish.ingredients;
    this.formGroup = this.formBuilder.group({
      id: '',
      name: '',
      price: '',
      ingredient: ''
    }, null);
  }

  addIngredient(ingredient: String) {
    if (this.formGroup.get('ingredient').value != "") {
      this.ingredients.push(this.formGroup.get('ingredient').value);
      this.formGroup.get('ingredient').setValue("");
    }
    else {
      this._snackBar.open("Invalid ingredient name!", "Close", {
        duration: 2000,
      });
    }
  }

  deleteIngredient(ingredient: String) {
    delete this.ingredients[this.ingredients.findIndex(x => x == ingredient)];
    this.ingredients = this.ingredients.filter(value => Object.keys(value).length !== 0);
    console.log(this.ingredients);
  }

  backClicked() {
    this._location.back();
  }

  get id() {
    return this.dish.id;
  }

  get name() {
    return this.formGroup.get('name');
  }

  get price() {
    return this.formGroup.get('price');
  }

  get ingredient() {
    return this.formGroup.get('ingredient');
  }

  modifyItem() {
    this.modifiedDish.id = this.dish.id;
    this.modifiedDish.name = this.formGroup.get('name').value;
    this.modifiedDish.ingredients = this.ingredients;
    console.log(this.modifiedDish);
  }
}

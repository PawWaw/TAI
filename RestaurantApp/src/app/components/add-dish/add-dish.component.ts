import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Dish } from '../_models/Dish';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-dish',
  templateUrl: './add-dish.component.html',
  styleUrls: ['./add-dish.component.css']
})
export class AddDishComponent implements OnInit {

  formGroup: FormGroup;
  dish: Dish;
  ingredients: String[] = [];
  value = 'Clear me';

  constructor(
    private _location: Location,
    private _snackBar: MatSnackBar,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      name: '',
      price: '',
      ingredient: ''
    }, null);
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
    delete this.ingredients[this.ingredients.findIndex(x => x==ingredient)];
    this.ingredients = this.ingredients.filter(value => Object.keys(value).length !== 0);
  }

  backClicked() {
    this._location.back();
  }

  addItem() {
    this.dish.name = this.formGroup.get('name').value;
    this.dish.ingredients = this.ingredients;
    console.log(this.dish);
  }
}

import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Dish } from '../_models/Dish';
import { Ingredient } from '../_models/Ingredient';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DishService } from '../_services/dish.service';
import { Router } from '@angular/router';

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
    private dishService: DishService,
    private router: Router
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
    this.modifiedDish.price = this.dish.price;
    this.modifiedDish.name = this.formGroup.get('name').value;
    this.modifiedDish.ingredients = this.ingredients;
    this.dishService.modifyDish(this.modifiedDish).subscribe(
      data=>{
        this.router.navigate(['/dish']);
      },
      error=>{
      }
    );
  }

  deleteItem() {
    this.dishService.deleteDish(this.dish.id).subscribe(
      data=>{
        this.router.navigate(['/dish']);
      },
      error=>{
      }
    );
  }
}

import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Dish } from '../_models/Dish';
import { DishService } from '../_services/dish.service';

@Component({
  selector: 'app-dish',
  templateUrl: './dish.component.html',
  styleUrls: ['./dish.component.css']
})
export class DishComponent implements OnInit {

  dishes: Dish[];
  data: any;
  dataSource: any;
  displayedColumns: string[] = ['id', 'name', 'price', 'action'];

  constructor(
    private router: Router,
    private _snackBar: MatSnackBar,
    private dishService: DishService
  ) { }

  ngOnInit() {
    // if (localStorage.getItem('current_user') == null) { // TODO: odkomentować po podpięciu backendu
    //   this.router.navigate(['/signin']);
    //   this._snackBar.open("Sign in to do this operation!", "Close", {
    //   duration: 2000,
    // });
    // }
    this.dataSource = this.dishService.getDishes();
  }

  getRecord(row: Dish) {
    localStorage.setItem('row', JSON.stringify(row));
    this.router.navigate(['/dish/details', {id: row.id}]);
  }

  editRecord(row: Dish) {
    localStorage.setItem('row', JSON.stringify(row));
    this.router.navigate(['dish/modify', {id: row.id}]);
  }

  addDish() {
    this.router.navigate(['dish/create']);
  }
}

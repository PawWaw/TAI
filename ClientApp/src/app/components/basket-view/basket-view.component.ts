import { Component, ViewChild } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ModelService } from "src/app/core/services/model.service";
import { Dish, DishesInBasket } from "src/app/shared/models/dish.interface";

import { BasketViewService } from "./basket-view.service";

@Component({
  selector: 'app-basket-view',
  templateUrl: './basket-view.component.html',
  styleUrls: ['./basket-view.component.scss']
})
export class BasketViewComponent {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  private _dishOrdered: DishesInBasket[];

  displayedColumns: string[] = ["restaurantName", "name", "price", "count", "action"];
  dataSource: MatTableDataSource<DishesInBasket>;

  simpleUserDataForm = this._fb.group({
    firstName: [null],
    lastName: [null],
    city: [null],
    address: [null]
  });

  constructor(
    private _fb: FormBuilder,
    private _modelService: ModelService,
    private _basketViewService: BasketViewService) {
    this.simpleUserDataForm.patchValue({
      firstName: _modelService.getUserFirstName(),
      lastName: _modelService.getUserLastName(),
      city: _modelService.getUserCity(),
      address: _modelService.getUserAddress()
    });

    this._dishOrdered = _modelService.getOrderedDishes();
    this.dataSource = new MatTableDataSource(this._dishOrdered);
  }

  deleteElement(elem: DishesInBasket): void {
    this._dishOrdered = this._modelService.deleteDish(elem);
    this.dataSource = new MatTableDataSource(this._dishOrdered);
  }

  removeOne(elem: DishesInBasket): void {
    this._dishOrdered = this._modelService.simpleRemoveOneDishFromOrder(elem);
  }

  addOne(elem: DishesInBasket): void {
    this._dishOrdered = this._modelService.simpleAddOneDishToOrder(elem);
  }

  submitOrder(): void {

    this._basketViewService.submitOrder(this._modelService.getOrderedDishes());
    this._modelService.deleteAllDishesInBasket();
  }

  getRestaurantName(dish: Dish): string {
    return dish.restaurantName + ", " + dish.city + ", " + dish.address;
  }

}

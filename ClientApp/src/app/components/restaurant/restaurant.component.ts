import { Component, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { OrderStation } from "src/app/shared/models/restaurant.interface";

import { RestaurantViewService } from "./restaurant-view.service";

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.scss']
})
export class RestaurantComponent {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  private _restaurantsData: OrderStation[];

  displayedColumns: string[] = ["name", "city", "address", "menu"];
  dataSource: MatTableDataSource<OrderStation>;

  constructor(private _restaurantViewService: RestaurantViewService, private _router: Router) {
    this._restaurantViewService.getRestaurantsMock().subscribe((restaurants: OrderStation[]) => {
      this._restaurantsData = restaurants;
    });

    this.dataSource = new MatTableDataSource(this._restaurantsData);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  viewRestaurantDishes(element: OrderStation): void {
    console.log(element);

    this._router.navigate(["restaurant", element.id]);
  }

}

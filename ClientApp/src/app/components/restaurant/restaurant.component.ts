import { Component, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { WsOrderStation } from "src/app/shared/models/restaurant.interface";

import { RestaurantViewService } from "./restaurant-view.service";

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.scss']
})
export class RestaurantComponent {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  private _restaurantsData: WsOrderStation[];

  displayedColumns: string[] = ["name", "city", "address", "menu"];
  dataSource: MatTableDataSource<WsOrderStation>;

  constructor(
    private _restaurantViewService: RestaurantViewService,
    private _router: Router) {
    // let test: WsOrderStation[] = [];

    // this.dataSource = new MatTableDataSource(test);
    this._restaurantViewService.getRestaurants().subscribe((restaurants: WsOrderStation[]): void => {
      this._restaurantsData = restaurants;
      this.dataSource = new MatTableDataSource(restaurants);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });


  }

  ngAfterViewInit() {

    // this.dataSource.paginator = this.paginator;
    // this.dataSource.sort = this.sort;

  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  viewRestaurantDishes(element: WsOrderStation): void {
    this._router.navigate(["restaurant", element.id]);
  }

}

import { AfterViewInit, Component, Input, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ModelService } from "src/app/core/services/model.service";
import { Dish, DishWithRate } from "src/app/shared/models/dish.interface";
import { OrderStation } from "src/app/shared/models/restaurant.interface";

import { DishViewService } from "./dish-view.service";

@Component({
  selector: 'app-dish-view',
  templateUrl: './dish-view.component.html',
  styleUrls: ['./dish-view.component.scss']
})
export class DishViewComponent implements OnInit, AfterViewInit {
  @Input() viewType: number;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  private _dishData: Dish[];

  displayedColumns: string[] = ["restaurantName", "dishName", "ingredients", "price", "rate", "addToBasket"];
  dataSource: MatTableDataSource<Dish>;
  userLoggedin = false;

  constructor(private _dishViewService: DishViewService, private _modelService: ModelService) {
    this.userLoggedin = _modelService.getUserLoggedin() ?? false;
  }

  ngOnInit(): void {
    if (this.viewType === -1) {
      this._dishViewService.getDishesMock().subscribe((orderStations: OrderStation[]) => {
        let dishes: Dish[] = [];
        orderStations.forEach((orderStation: OrderStation) => {
          orderStation.restaurant.food.forEach((x: DishWithRate) => {
            dishes.push({
              id: x.id,
              orderStationId: orderStation.id,
              restaurantId: orderStation.restaurant.id,
              name: x.name,
              price: x.price,
              ingredients: x.ingredients,
              restaurantName: orderStation.restaurant.name,
              city: orderStation.city,
              address: orderStation.address,
              rate: x.rate
            })
          });
        });
        this._dishData = dishes
      });
    }
    else {
      this._dishViewService.getDishesByRestaurantMock(this.viewType).subscribe((orderStation: OrderStation) => {
        let dishes: Dish[] = [];
        orderStation.restaurant.food.forEach((x: DishWithRate) => {
          dishes.push({
            id: x.id,
            orderStationId: orderStation.id,
            restaurantId: orderStation.restaurant.id,
            name: x.name,
            price: x.price,
            ingredients: x.ingredients,
            restaurantName: orderStation.restaurant.name,
            city: orderStation.city,
            address: orderStation.address,
            rate: x.rate
          })
        });
        this._dishData = dishes
      });
    }

    this.dataSource = new MatTableDataSource(this._dishData);
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

  addToBasket(element: Dish): void {
    this._modelService.addDishToOrder(element);
  }

  getRestaurantName(dish: Dish): string {
    return dish.restaurantName + ", " + dish.city + ", " + dish.address;
  }

  getIngredients(dish: Dish): string {
    let s = "";
    dish.ingredients.forEach((x: string) => {
      s += x + " "
    });
    return s;
  }
}

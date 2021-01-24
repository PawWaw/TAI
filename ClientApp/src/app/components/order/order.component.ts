import { animate, state, style, transition, trigger } from "@angular/animations";
import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { ModelService } from "src/app/core/services/model.service";
import { Food } from "src/app/shared/models/dish.interface";
import { WsOrder } from "src/app/shared/models/order.interface";
import { WsOrderStation } from "src/app/shared/models/restaurant.interface";

import { RateDelivererViewComponent } from "../rate-deliverer-view/rate-deliverer-view.component";
import { RateDishViewComponent } from "../rate-dish-view/rate-dish-view.component";
import { OrderViewService } from "./order-view.service";

export interface SpecialFood {
  id: number;
  name: string;
  price: number;
  count: number;
  restaurantName: string;
  city: string;
  address: string;
}

export class SpecialOrder {
  orderData: WsOrder;
  dataSource: MatTableDataSource<SpecialFood>;
}

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class OrderComponent implements OnInit {

  displayedColumns: string[] = ["dish", "price", "count", "rateDish"];
  specialOrder: SpecialOrder[] = [];
  userLoggedin = false;

  constructor(
    private _modelService: ModelService,
    private _router: Router,
    private _orderViewService: OrderViewService,
    public dialog: MatDialog) {
    this.userLoggedin = _modelService.getUserLoggedin() ?? false;
    if (this.userLoggedin === false) {
      this._router.navigate([""]);
    }

    _orderViewService.getUserOrders().subscribe((x: WsOrder[]) => {
      x.forEach((o: WsOrder) => {
        let foods: SpecialFood[] = [];

        o.wsFood.forEach((f: Food) => {

          let dishes = foods.filter(x => x.id === f.id);
          if (dishes.length === 0) {
            foods.push({
              id: f.id,
              name: f.name,
              price: f.price,
              count: 1,
              restaurantName: o.wsOrderStation.wsRestaurant.name,
              city: o.wsOrderStation.city,
              address: o.wsOrderStation.address
            });
          }
          else {
            let index = foods.indexOf(dishes[0]);
            foods[index].count++;
            foods[index].price = foods[index].count * f.price;
          }
        });

        this.specialOrder.push({
          orderData: o,
          dataSource: new MatTableDataSource(foods)
        });
      })

    });
  }

  ngOnInit(): void {
  }

  getRestaurantName(orderStation: WsOrderStation): string {
    return orderStation.wsRestaurant.name + ", " + orderStation.city + ", " + orderStation.address;
  }

  getOrderInfo(order: WsOrder): string {
    return this.getStatus(order.status) + " " + this.dateAsYYYYMMDDHHMM(order.startTime) + " - " + this.dateAsYYYYMMDDHHMM(order.endTime);
  }

  getStatus(status: string): string {
    let s = "";
    switch (status) {
      case "ENDED": {
        s = "ended";
        break;
      }
      case "REALIZE": {
        s = "realize";
        break;
      }
      case "STARTED": {
        s = "started";
        break;
      }
    }

    return s;
  }

  dateAsYYYYMMDDHHMM(dateString: string): string {
    let date = new Date(dateString);
    if (dateString === null || dateString === undefined) {
      return "...";
    }
    return date.getFullYear() + '-' +
      ((date.getMonth() + 1 < 10) ? ("0" + (date.getMonth() + 1)) : (date.getMonth() + 1)) + '-' +
      ((date.getDate() < 10) ? ("0" + date.getDate()) : (date.getDate())) + ' ' +
      ((date.getHours() < 10) ? ("0" + date.getHours()) : (date.getHours())) + ':' +
      ((date.getMinutes() < 10) ? ("0" + date.getMinutes()) : (date.getMinutes()));
  }

  rateDeliverer(order: WsOrder) {

    const dialogRef = this.dialog.open(RateDelivererViewComponent, {
      width: "40%",
      data: order
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }

  rateDish(food: SpecialFood) {
    const dialogRef = this.dialog.open(RateDishViewComponent, {
      width: "40%",
      data: food
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }

  showRateDishOrDelivererButton(status: string): boolean {
    if (status === "STARTED" || status === "REALIZE") {
      return false;
    }
    else {
      return true;
    }
  }
}

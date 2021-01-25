import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { DishRate, RatePick } from "src/app/shared/models/order.interface";

import { OrderViewService } from "../order/order-view.service";
import { SpecialFood } from "../order/order.component";

@Component({
  selector: 'app-rate-dish-view',
  templateUrl: './rate-dish-view.component.html',
  styleUrls: ['./rate-dish-view.component.scss']
})
export class RateDishViewComponent {

  ratePick: RatePick[] = [
    { value: 0, typeName: "No rate" },
    { value: 1, typeName: "1/6" },
    { value: 2, typeName: "2/6" },
    { value: 3, typeName: "3/6" },
    { value: 4, typeName: "4/6" },
    { value: 5, typeName: "5/6" },
    { value: 6, typeName: "6/6" }
  ];

  selectedRate = this.ratePick[0].value;

  constructor(
    private _orderViewService: OrderViewService,
    public dialogRef: MatDialogRef<RateDishViewComponent>,
    @Inject(MAT_DIALOG_DATA) public food: SpecialFood) { }

  rateDish() {
    if (this.selectedRate !== 0) {
      let dishRate: DishRate = {
        id: this.food.id,
        rate: this.selectedRate
      }
      this._orderViewService.rateDish(dishRate).subscribe((): void => {});
    }
  }
}

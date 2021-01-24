import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { DelivererRate, RatePick, WsOrder } from "src/app/shared/models/order.interface";

import { OrderViewService } from "../order/order-view.service";

@Component({
  selector: 'app-rate-deliverer-view',
  templateUrl: './rate-deliverer-view.component.html',
  styleUrls: ['./rate-deliverer-view.component.scss']
})
export class RateDelivererViewComponent {

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
    public dialogRef: MatDialogRef<RateDelivererViewComponent>,
    @Inject(MAT_DIALOG_DATA) public order: WsOrder) { }

  rateDeliverer() {
    if (this.selectedRate !== 0) {
      let delivererRate: DelivererRate = {
        id: this.order.delivererId,
        rate: this.selectedRate
      }
      this._orderViewService.rateDeliverer(delivererRate);
    }
  }

}

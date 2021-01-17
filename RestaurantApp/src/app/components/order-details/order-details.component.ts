import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Order } from '../_models/Order';
import { Router } from '@angular/router';
import { OrderService } from '../_services/order.service';
import { MatSelectChange } from '@angular/material';
import { Deliverer } from '../_models/Deliverer';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  formGroup: FormGroup;
  order: Order;
  isDelivered: boolean;
  deliverers: Observable<Deliverer[]>;
  selectedData: string
  status: string

  constructor(
    private router: Router,
    private _location: Location,
    private formBuilder: FormBuilder,
    private orderService: OrderService
    ) { }

  ngOnInit() {
    this.deliverers = this.orderService.getDeliverers();
    this.order = JSON.parse(localStorage.getItem('row'));
    console.log(this.deliverers);
    if (this.order.deliverer == null) {
      this.isDelivered = false;
    } else {
      this.isDelivered = true;
    }

    this.formGroup = this.formBuilder.group({
      id: '',
      orderStation: '',
      startTime: '',
      endTime: '',
      deliverer: '',
      status: ''
    }, null);
  }

  get id() {
    return this.order.id;
  }

  get orderStation() {
    return this.formGroup.get('orderStation');
  }

  get startTime() {
    return this.formGroup.get('startTime');
  }

  get endTime() {
    return this.formGroup.get('endTime');
  }

  get deliverer() {
    return this.formGroup.get('deliverer');
  }

  backClicked() {
    this._location.back();
  }

  modifyItem() {
    this.order.delivererId = Number(this.selectedData);
    this.order.status = this.status;
    this.orderService.modifyOrder(this.order).subscribe(
      data=>{
        this.router.navigate(['']);
      },
      error=>{
      }
    );
  }

  selectedValue(event: MatSelectChange) {
    this.selectedData = event.value;
  }

  selectedStatus(event: MatSelectChange) {
    this.status = event.value;
  }
}

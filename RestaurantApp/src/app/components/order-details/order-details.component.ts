import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Order } from '../_models/Order';

@Component({
  selector: 'app-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  formGroup: FormGroup;
  order: Order;

  constructor(
    private _location: Location,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit() {
    this.order = JSON.parse(localStorage.getItem('row'));
  }

  backClicked() {
    this._location.back();
  }
}

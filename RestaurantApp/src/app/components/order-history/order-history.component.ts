import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Order } from '../_models/Order';
import { OrderHistoryService } from '../_services/order-history.service';

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent implements OnInit {

  
  orders: Order[];
  data: any;
  dataSource: any;
  displayedColumns: string[] = ['id', 'status', 'deliverer', 'startTime', 'endTime'];

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(
    private router: Router,
    private _snackBar: MatSnackBar,
    private orderHistoryService: OrderHistoryService
  ) { }

  ngOnInit() {
    // if (localStorage.getItem('current_user') == null) { // TODO: odkomentować po podpięciu backendu
    //   this.router.navigate(['/signin']);
    //   this._snackBar.open("Sign in to do this operation!", "Close", {
    //   duration: 2000,
    // });
    // }
    this.dataSource = this.orderHistoryService.getOrders();
  }
}

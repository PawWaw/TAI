import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Order } from '../_models/Order';
import { OrderService } from '../_services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  orders: Order[];
  data: any;
  dataSource: any;
  displayedColumns: string[] = ['id', 'status', 'deliverer', 'startTime', 'endTime', 'action'];

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(
    private router: Router,
    private _snackBar: MatSnackBar,
    private orderService: OrderService
  ) { }

  ngOnInit() {
    // if (localStorage.getItem('current_user') == null) { // TODO: odkomentować po podpięciu backendu
    //   this.router.navigate(['/signin']);
    //   this._snackBar.open("Sign in to do this operation!", "Close", {
    //   duration: 2000,
    // });
    // }
    this.dataSource = this.orderService.getOrders();
  }

  getRecord(row: Order) {
    localStorage.setItem('row', JSON.stringify(row));
    this.router.navigate(['/order/details', {id: row.id}]);
  }
}

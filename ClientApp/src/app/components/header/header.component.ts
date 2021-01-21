import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { ModelService } from "src/app/core/services/model.service";
import { User } from "src/app/shared/models/user.interface";

import { BasketViewComponent } from "../basket-view/basket-view.component";
import { UserDataComponent } from "../user-data/user-data.component";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

  userLoggedin = false;
  elementsInBasket: string = "";

  constructor(private _router: Router, private _modelService: ModelService, public dialog: MatDialog) {
    _modelService.loggedin$.subscribe((x: boolean): void => {
      this.userLoggedin = x;
    });
    _modelService.elementsInBasket$.subscribe((x: string): void => {
      this.elementsInBasket = x;
    });
  }

  moveTo(path: string): void {
    this._router.navigate([path]);
  }

  showBasket(): void {
    const dialogRef = this.dialog.open(BasketViewComponent, {
      width: "70%"
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }

  showUserData(): void {
    let user: User = this._modelService.getUser();

    const dialogRef = this.dialog.open(UserDataComponent, {
      width: "70%",
      data: user
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }

  logout() {
    this._modelService.clearData();
    this._router.navigate([""]);
  }
}

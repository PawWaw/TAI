import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-dish-by-restaurant',
  templateUrl: './dish-by-restaurant.component.html',
  styleUrls: ['./dish-by-restaurant.component.scss']
})
export class DishByRestaurantComponent {

  id: number;

  constructor(private _route: ActivatedRoute) {
    this._route.params.subscribe(params => {
      if (params["id"]) {
        this.id = params["id"];
      }
    });
  }
}

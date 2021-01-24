import { Component, OnInit } from '@angular/core';
import { DataSharingService } from '../_services/data-sharing.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isLogged: boolean = false;

  constructor(private dataSharingService: DataSharingService) { 
    this.dataSharingService.isLogged.subscribe( value => {
      this.isLogged = value;
  });
  }

  ngOnInit(): void {
    if (localStorage.getItem('auth_token') != null) { 
      this.isLogged = true;
    }
  }

  logout(){
    localStorage.removeItem('auth_token');
    this.isLogged = false;
  }

}

import { Component, Input, OnInit } from '@angular/core';
import { VendorUser } from 'src/app/core/models';

@Component({
  selector: 'app-admin-info',
  templateUrl: './admin-info.component.html',
  styleUrls: ['./admin-info.component.css']
})
export class AdminInfoComponent implements OnInit {

  @Input() public user: VendorUser;

  constructor() { }

  ngOnInit(): void {
  }

}

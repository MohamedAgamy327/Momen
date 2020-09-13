import { Component, Input } from '@angular/core';
import { VendorUser } from 'src/app/core/models';

@Component({
  selector: 'app-admin-info',
  templateUrl: './admin-info.component.html',
  styleUrls: ['./admin-info.component.css']
})
export class AdminInfoComponent {

  @Input() public user: VendorUser;

}

import { Component, Input } from '@angular/core';
import { Vendor } from 'src/app/core/models';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-corporation-info',
  templateUrl: './corporation-info.component.html',
  styleUrls: ['./corporation-info.component.css']
})
export class CorporationInfoComponent {

  @Input() public vendor: Vendor;
  environment = environment;

}

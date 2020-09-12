import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Vendor, VendorUser } from 'src/app/core/models';
import { VendorService, VendorUserService } from 'src/app/core/services';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css']
})
export class StatusComponent implements OnInit {

  vendor: Vendor;
  user: VendorUser;

  constructor(
    private vendorService: VendorService,
    private vendorUserService: VendorUserService,
    public route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getVendor(Number(this.route.snapshot.params.vendorId));
    this.getAdmin(Number(this.route.snapshot.params.vendorId));
  }

  getVendor(vendorId: number) {
    this.vendorService.get(vendorId).subscribe(
      (res: any) => {
        this.vendor = res;
      });
  }

  getAdmin(vendorId: number) {
    this.vendorUserService.getAdmin(vendorId).subscribe(
      (res: any) => {
        this.user = res;
      });
  }

}

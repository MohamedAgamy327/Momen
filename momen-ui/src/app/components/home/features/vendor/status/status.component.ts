import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Vendor } from 'src/app/core/models';
import { VendorService } from 'src/app/core/services';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css']
})
export class StatusComponent implements OnInit {

  vendor: Vendor;

  constructor(
    private vendorService: VendorService,
    public route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getVendor(Number(this.route.snapshot.params.vendorId));
  }

  getVendor(vendorId: number) {
    this.vendorService.get(vendorId).subscribe(
      (res: any) => {
        this.vendor = res;
      });
  }

}

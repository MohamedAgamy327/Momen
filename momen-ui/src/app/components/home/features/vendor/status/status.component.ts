import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Vendor, VendorPicture, VendorUser } from 'src/app/core/models';
import { VendorPictureService, VendorService, VendorUserService } from 'src/app/core/services';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css']
})

export class StatusComponent implements OnInit {

  vendor: Vendor;
  user: VendorUser;
  vendorPictures: VendorPicture[];


  constructor(
    private vendorService: VendorService,
    private vendorUserService: VendorUserService,
    private vendorPictureService: VendorPictureService,
    public route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.getVendor(Number(this.route.snapshot.params.vendorId));
    this.getAdmin(Number(this.route.snapshot.params.vendorId));
    this.getVendorPictures(Number(this.route.snapshot.params.vendorId));
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

  getVendorPictures(id: number) {
    this.vendorPictureService.getAll(id).subscribe(
      (res: any) => {
        this.vendorPictures = res;
      });
  }


}

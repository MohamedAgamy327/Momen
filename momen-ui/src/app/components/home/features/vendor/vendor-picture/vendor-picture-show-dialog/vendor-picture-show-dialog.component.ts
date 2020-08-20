import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-vendor-picture-show-dialog',
  templateUrl: './vendor-picture-show-dialog.component.html',
  styleUrls: ['./vendor-picture-show-dialog.component.css']
})

export class VendorPictureShowDialogComponent {

  environment = environment;

  constructor(
    public dialogRef: MatDialogRef<VendorPictureShowDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

}

import { VendorPicture } from 'src/app/core/models';
import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { PictureDialogComponent } from 'src/app/components/home';

@Component({
  selector: 'app-vendor-pictures-show',
  templateUrl: './vendor-pictures-show.component.html',
  styleUrls: ['./vendor-pictures-show.component.css']
})

export class VendorPicturesShowComponent implements OnInit {

  filter: string;

  displayedColumns: string[] = ['fileName', 'actions'];
  @Input() public vendorPictures: VendorPicture[];
  dataSource = new MatTableDataSource<VendorPicture>();

  constructor(
    private dialog: MatDialog,
  ) {
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.vendorPictures);
  }

  show(file: any) {
    this.dialog.open(PictureDialogComponent, {
      data: { path: file.picturePath, title: 'Vendor Picture' }
    });
  }

}

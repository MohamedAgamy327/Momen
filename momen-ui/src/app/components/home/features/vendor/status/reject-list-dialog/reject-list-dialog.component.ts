import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { VendorReject } from 'src/app/core/models';

@Component({
  selector: 'app-reject-list-dialog',
  templateUrl: './reject-list-dialog.component.html',
  styleUrls: ['./reject-list-dialog.component.css']
})
export class RejectListDialogComponent implements OnInit {

  displayedColumns: string[] = ['reason', 'date'];
  // vendorRejects: VendorReject[];
  dataSource = new MatTableDataSource<VendorReject>();

  constructor(
    public dialogRef: MatDialogRef<RejectListDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
  ) {
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.data);
  }

}




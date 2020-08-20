import { VendorPictureService, PageTitleService } from 'src/app/core/services';
import { VendorPicture } from 'src/app/core/models';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { DeleteDialogComponent, PictureDialogComponent } from 'src/app/components/home';

@Component({
  selector: 'app-vendor-pictures',
  templateUrl: './vendor-pictures.component.html',
  styleUrls: ['./vendor-pictures.component.css']
})

export class VendorPicturesComponent implements OnInit {

  filter: string;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['fileName', 'actions'];
  vendorPictures: VendorPicture[];
  dataSource = new MatTableDataSource<VendorPicture>();

  constructor(
    public route: ActivatedRoute,
    public router: Router,
    private toastrService: ToastrService,
    private vendorPictureService: VendorPictureService,
    private dialog: MatDialog,
    private pageTitleService: PageTitleService
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Vendor Picture');
    this.getVendorPictures(this.route.snapshot.params.vendorId);
  }

  getVendorPictures(id: number) {
    this.vendorPictureService.getAll(id).subscribe(
      (res: any) => {
        this.vendorPictures = res;
        this.refreshData();
      });
  }

  refreshData() {
    this.dataSource = new MatTableDataSource(this.vendorPictures);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  clearFilter() {
    this.filter = '';
    this.applyFilter(this.filter);
  }

  showDelete(vendorPicture: VendorPicture) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { type: 'this vendorPicture' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(vendorPicture.id);
      }
    });
  }

  delete(id: number) {
    this.vendorPictureService.delete(id).subscribe(
      (res: any) => {
        this.toastrService.success('Deleted Successfully', 'Delete');
        const index = this.vendorPictures.findIndex(f => f.id === res.id);
        this.vendorPictures.splice(index, 1);
        this.refreshData();
      });
  }

  show(file: any) {
    this.dialog.open(PictureDialogComponent, {
      data: { path: file.picturePath, title: 'Vendor Picture' }
    });
  }

}

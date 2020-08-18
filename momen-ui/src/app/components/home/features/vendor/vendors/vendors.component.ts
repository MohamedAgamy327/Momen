import { PageTitleService } from 'src/app/core/services';
import { Vendor } from 'src/app/core/models';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DeleteDialogComponent } from 'src/app/components/home';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { VendorService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class VendorsComponent implements OnInit {

  filter: string;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'email', 'phone', 'category', 'action'];
  vendors: Vendor[];
  dataSource = new MatTableDataSource<Vendor>();

  constructor(
    private pageTitleService: PageTitleService,
    private vendorService: VendorService,
    private toastrService: ToastrService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Vendors');
    this.getVendors();
  }

  getVendors() {
    this.vendorService.getAll().subscribe(
      (res: any) => {
        this.vendors = res;
        this.refreshData();
      });
  }

  refreshData() {
    this.dataSource = new MatTableDataSource(this.vendors);
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

  showDelete(vendor: Vendor) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { type: 'this vendor' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(vendor.id);
      }
    });
  }

  delete(id: number) {
    this.vendorService.delete(id).subscribe(
      (res: any) => {
        this.toastrService.success('Deleted Successfully', 'Delete');
        const index = this.vendors.findIndex(f => f.id === res.id);
        this.vendors.splice(index, 1);
        this.refreshData();
      });
  }

}

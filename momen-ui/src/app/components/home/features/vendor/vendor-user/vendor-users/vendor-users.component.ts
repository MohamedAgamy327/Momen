import { PageTitleService } from 'src/app/core/services';
import { VendorUser } from 'src/app/core/models';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DeleteDialogComponent } from 'src/app/components/home';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { VendorUserService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vendor-users',
  templateUrl: './vendor-users.component.html',
  styleUrls: ['./vendor-users.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class VendorUsersComponent implements OnInit {

  filter: string;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'email', 'phone', 'role', 'action'];
  vendorUsers: VendorUser[];
  dataSource = new MatTableDataSource<VendorUser>();

  constructor(
    private pageTitleService: PageTitleService,
    private vendorUserService: VendorUserService,
    private toastrService: ToastrService,
    private dialog: MatDialog,
    public route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Vendor Users');
    this.getVendorUsers(Number(this.route.snapshot.params.vendorId));
  }

  getVendorUsers(vendorId: number) {
    this.vendorUserService.getAll(vendorId).subscribe(
      (res: any) => {
        this.vendorUsers = res;
        this.refreshData();
      });
  }

  refreshData() {
    this.dataSource = new MatTableDataSource(this.vendorUsers);
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

  showDelete(user: VendorUser) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { type: 'this Vendor User' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(user.id);
      }
    });
  }

  delete(id: number) {
    this.vendorUserService.delete(id).subscribe(
      (res: any) => {
        this.toastrService.success('Deleted Successfully', 'Delete');
        const index = this.vendorUsers.findIndex(f => f.id === res.id);
        this.vendorUsers.splice(index, 1);
        this.refreshData();
      });
  }

}

import { PageTitleService } from 'src/app/core/services';
import { VendorUser } from 'src/app/core/models';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DeleteDialogComponent, ConfirmDialogComponent } from 'src/app/components/home';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { VendorUserService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { VendorUserAddDialogComponent } from '../vendor-user-add-dialog/vendor-user-add-dialog.component';
import { VendorUserEditDialogComponent } from '../vendor-user-edit-dialog/vendor-user-edit-dialog.component';

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

  showAdd() {
    const dialogRef = this.dialog.open(VendorUserAddDialogComponent, {
      data: Number(this.route.snapshot.params.vendorId)
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.vendorUsers.unshift(result);
        this.refreshData();
      }
    });
  }

  showEdit(vendorUser: VendorUser) {
    const dialogRef = this.dialog.open(VendorUserEditDialogComponent, {
      data: vendorUser
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const index = this.vendorUsers.findIndex(f => f.id === result.id);
        this.vendorUsers[index] = result;
        this.refreshData();
      }
    });
  }

  showDelete(vendorUser: VendorUser) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { type: 'this Vendor User' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(vendorUser.id);
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

  showResetPasswordConfirm(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { question: 'Do you want to confirm reseting password?', title: 'Reset' }

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.resetPassword(id);
      }
    });
  }

  resetPassword(id: number) {
    this.vendorUserService.resetPassword(id).subscribe(
      (res: any) => {
        this.toastrService.success('Reset Password Successfully', 'Reset');
        const index = this.vendorUsers.findIndex(f => f.id === res.id);
        this.vendorUsers.splice(index, 1);
        this.refreshData();
      });
  }

}

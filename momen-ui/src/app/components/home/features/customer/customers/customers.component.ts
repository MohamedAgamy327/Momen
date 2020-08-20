import { PageTitleService } from 'src/app/core/services';
import { Customer } from 'src/app/core/models';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DeleteDialogComponent, ConfirmDialogComponent, PictureDialogComponent } from 'src/app/components/home';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { CustomerService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { CustomerAddDialogComponent } from '../customer-add-dialog/customer-add-dialog.component';
import { CustomerEditDialogComponent } from '../customer-edit-dialog/customer-edit-dialog.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class CustomersComponent implements OnInit {

  filter: string;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'email', 'phone', 'isBlocked', 'action'];
  customers: Customer[];
  dataSource = new MatTableDataSource<Customer>();

  constructor(
    private pageTitleService: PageTitleService,
    private customerService: CustomerService,
    private toastrService: ToastrService,
    private dialog: MatDialog,
    public route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Customers');
    this.getCustomers();
  }

  getCustomers() {
    this.customerService.getAll().subscribe(
      (res: any) => {
        this.customers = res;
        this.refreshData();
      });
  }

  refreshData() {
    this.dataSource = new MatTableDataSource(this.customers);
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
    const dialogRef = this.dialog.open(CustomerAddDialogComponent, {
      data: Number(this.route.snapshot.params.vendorId)
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.customers.unshift(result);
        this.refreshData();
      }
    });
  }

  show(customer: any) {
    this.dialog.open(PictureDialogComponent, {
      data: { path: customer.picturePath, title: 'Customer Picture' }
    });
  }

  showEdit(customer: Customer) {
    const dialogRef = this.dialog.open(CustomerEditDialogComponent, {
      data: customer
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const index = this.customers.findIndex(f => f.id === result.id);
        this.customers[index] = result;
        this.refreshData();
      }
    });
  }

  showDelete(customer: Customer) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { type: 'this customer' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(customer.id);
      }
    });
  }

  delete(id: number) {
    this.customerService.delete(id).subscribe(
      (res: any) => {
        this.toastrService.success('Deleted Successfully', 'Delete');
        const index = this.customers.findIndex(f => f.id === res.id);
        this.customers.splice(index, 1);
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
    this.customerService.resetPassword(id).subscribe(
      (res: any) => {
        this.toastrService.success('Reset Password Successfully', 'Reset');
      });
  }

  showBlockConfirm(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { question: 'Do you want to confirm block this customer?', title: 'Block' }

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.block(id);
      }
    });
  }

  block(id: number) {
    this.customerService.block(id).subscribe(
      (res: any) => {
        this.toastrService.success('Blocked Password Successfully', 'Block');
        const index = this.customers.findIndex(f => f.id === res.id);
        this.customers[index] = res;
        this.refreshData();
      });
  }

  showUnblockConfirm(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { question: 'Do you want to confirm unblock this customer?', title: 'Unblock' }

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.unblock(id);
      }
    });
  }

  unblock(id: number) {
    this.customerService.unblock(id).subscribe(
      (res: any) => {
        this.toastrService.success('Unblocked Password Successfully', 'Unblock');
        const index = this.customers.findIndex(f => f.id === res.id);
        this.customers[index] = res;
        this.refreshData();
      });
  }

}

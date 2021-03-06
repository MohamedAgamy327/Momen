import { PageTitleService } from 'src/app/core/services';
import { Contract } from 'src/app/core/models';
import { ContractEditDialogComponent } from '../contract-edit-dialog/contract-edit-dialog.component';
import { ContractAddDialogComponent } from '../contract-add-dialog/contract-add-dialog.component';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DeleteDialogComponent } from 'src/app/components/home';
import { environment } from 'src/environments/environment';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ContractService } from 'src/app/core/services/api/contract.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-contracts',
  templateUrl: './contracts.component.html',
  styleUrls: ['./contracts.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class ContractsComponent implements OnInit {

  environment = environment;

  filter: string;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'description', 'fileName', 'action'];
  contracts: Contract[];
  dataSource = new MatTableDataSource<Contract>();

  constructor(
    private pageTitleService: PageTitleService,
    private contractService: ContractService,
    private toastrService: ToastrService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Contracts');
    this.getContracts();
  }

  getContracts() {
    this.contractService.getAll().subscribe(
      (res: any) => {
        this.contracts = res;
        this.refreshData();
      });
  }

  refreshData() {
    this.dataSource = new MatTableDataSource(this.contracts);
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

  add() {
    const dialogRef = this.dialog.open(ContractAddDialogComponent, {
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contracts.unshift(result);
        this.refreshData();
      }
    });
  }

  edit(contract: Contract) {
    const dialogRef = this.dialog.open(ContractEditDialogComponent, {
      data: contract
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const index = this.contracts.findIndex(f => f.id === result.id);
        this.contracts[index] = result;
        this.refreshData();
      }
    });
  }

  showDelete(contract: Contract) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { type: 'this contract' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(contract.id);
      }
    });
  }

  delete(id: number) {
    this.contractService.delete(id).subscribe(
      (res: any) => {
        this.toastrService.success('Deleted Successfully', 'Delete');
        const index = this.contracts.findIndex(f => f.id === res.id);
        this.contracts.splice(index, 1);
        this.refreshData();
      });
  }

}

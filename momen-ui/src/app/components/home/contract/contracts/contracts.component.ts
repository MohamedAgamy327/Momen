import { RepositoryService, PageTitleService } from 'src/app/core/services';
import { Contract } from 'src/app/core/models';
import { ContractEditDialogComponent } from './../contract-edit-dialog/contract-edit-dialog.component';
import { ContractAddDialogComponent } from '../contract-add-dialog/contract-add-dialog.component';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatSnackBar, MatDialog } from '@angular/material';
import { DeleteDialogComponent } from '../../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-contracts',
  templateUrl: './contracts.component.html',
  styleUrls: ['./contracts.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class ContractsComponent implements OnInit {

  filter: string;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'description', 'fileName', 'action'];
  contracts: Contract[];
  dataSource = new MatTableDataSource<Contract>();

  constructor(
    private pageTitleService: PageTitleService,
    private repository: RepositoryService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Contracts');
    this.getContracts();
  }

  getContracts() {
    this.repository.get('contracts').subscribe(
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

  edit(contract) {
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

  delete(contract) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { id: contract.id, controller: 'contracts', type: 'this contract' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const index = this.contracts.findIndex(f => f.id === result.id);
        this.contracts.splice(index, 1);
        this.refreshData();
      }
    });
  }

}

import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSelect } from '@angular/material/select';
import { Router } from '@angular/router';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Contract, VendorReject } from 'src/app/core/models';
import { ContractService, VendorRejectService, VendorService } from 'src/app/core/services';
import { RejectListDialogComponent } from '../reject-list-dialog/reject-list-dialog.component';

@Component({
  selector: 'app-action-pending',
  templateUrl: './action-pending.component.html',
  styleUrls: ['./action-pending.component.css']
})

export class ActionPendingComponent implements OnInit, OnDestroy {

  action = 'accept';

  contracts: Contract[];
  @Input() public vendorId: number;

  public contractCtrl: FormControl = new FormControl('', Validators.required);
  public contractMultiFilterCtrl: FormControl = new FormControl();
  public filteredContractsMulti: ReplaySubject<Contract[]> = new ReplaySubject<Contract[]>();
  @ViewChild('singleContractSelect', { static: true }) singleContractSelect: MatSelect;

  public reasonCtrl: FormControl = new FormControl('', Validators.required);

  protected onDestroy = new Subject<void>();

  vendorRejects: VendorReject[];

  constructor(
    private contractService: ContractService,
    private vendorService: VendorService,
    private vendorRejectService: VendorRejectService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getContracts();
    this.getVendorRejects(Number(this.vendorId));
  }

  getVendorRejects(id: number) {
    this.vendorRejectService.getAll(id).subscribe(
      (res: any) => {
        this.vendorRejects = res;
        console.log(this.vendorRejects)
      });
  }

  getContracts() {
    this.contractService.getAll().subscribe(
      (res: any) => {
        this.contracts = res;
        this.getInitContracts();
      });
  }

  getInitContracts() {
    this.filteredContractsMulti.next(this.contracts.slice());
    this.contractMultiFilterCtrl.valueChanges
      .pipe(takeUntil(this.onDestroy))
      .subscribe(() => {
        this.filterContractsMulti();
      });
  }

  protected filterContractsMulti() {
    if (!this.contracts) {
      return;
    }
    let search = this.contractMultiFilterCtrl.value;
    if (!search) {
      this.filteredContractsMulti.next(this.contracts.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    this.filteredContractsMulti.next(
      this.contracts.filter(contract => contract.name.toLowerCase().indexOf(search) > -1)
    );
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  accept() {
    const accept = { id: Number(this.vendorId), contractId: this.contractCtrl.value };
    this.vendorService.acceptPending(Number(this.vendorId), accept).subscribe(
      (res: any) => {
        this.router.navigate([`/home/vendors`]);
      });
  }

  reject() {
    const reject = { vendorId: Number(this.vendorId), reason: this.reasonCtrl.value };
    this.vendorRejectService.create(reject).subscribe(
      (res: any) => {
        this.router.navigate([`/home/vendors`]);
      });
  }

  showRejects() {
    this.dialog.open(RejectListDialogComponent, {
      data: this.vendorRejects
    });
  }

}

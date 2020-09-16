import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSelect } from '@angular/material/select';
import { Router } from '@angular/router';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Contract } from 'src/app/core/models';
import { ContractService, VendorService } from 'src/app/core/services';

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

  protected onDestroy = new Subject<void>();

  constructor(
    private contractService: ContractService,
    private vendorService: VendorService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getContracts();
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

}

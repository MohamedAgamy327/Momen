
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { ContractRoutingModule } from './contract-routing.module';
import { ContractEditDialogComponent, ContractAddDialogComponent, ContractsComponent } from '.';


@NgModule({
  declarations: [
    ContractAddDialogComponent,
    ContractEditDialogComponent,
    ContractsComponent
  ],
  entryComponents: [
    ContractAddDialogComponent,
    ContractEditDialogComponent
  ],
  imports: [
    CommonModule,
    ContractRoutingModule,
    SharedModule,
    TranslateModule
  ],
  providers: [
  ]
})

export class ContractModule { }


import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerEditDialogComponent, CustomerAddDialogComponent, CustomersComponent } from '.';


@NgModule({
  declarations: [
    CustomerAddDialogComponent,
    CustomerEditDialogComponent,
    CustomersComponent
  ],
  entryComponents: [
    CustomerAddDialogComponent,
    CustomerEditDialogComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    SharedModule,
    TranslateModule
  ],
  providers: [
  ]
})

export class CustomerModule { }

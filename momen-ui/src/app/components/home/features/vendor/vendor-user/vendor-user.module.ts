
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { VendorUserRoutingModule } from './vendor-user-routing.module';
import { VendorUsersComponent, VendorUserAddDialogComponent, VendorUserEditDialogComponent } from '.';

@NgModule({
  declarations: [
    VendorUsersComponent,
    VendorUserAddDialogComponent,
    VendorUserEditDialogComponent
  ],
  entryComponents: [
    VendorUserAddDialogComponent,
    VendorUserEditDialogComponent
  ],
  imports: [
    CommonModule,
    VendorUserRoutingModule,
    SharedModule,
    TranslateModule
  ],
  providers: [
  ]
})

export class VendorUserModule { }


import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { VendorUserRoutingModule } from './vendor-user-routing.module';
import { VendorUsersComponent } from '.';

@NgModule({
  declarations: [
    VendorUsersComponent
  ],
  entryComponents: [

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

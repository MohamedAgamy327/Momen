
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { VendorRoutingModule } from './vendor-routing.module';
import { VendorsComponent, VendorAddComponent, StatusComponent, CorporationInfoComponent } from '.';

@NgModule({
  declarations: [
    VendorsComponent,
    VendorAddComponent,
    StatusComponent,
    CorporationInfoComponent,
  ],
  entryComponents: [

  ],
  imports: [
    CommonModule,
    VendorRoutingModule,
    SharedModule,
    TranslateModule
  ],
  providers: [
  ]
})

export class VendorModule { }

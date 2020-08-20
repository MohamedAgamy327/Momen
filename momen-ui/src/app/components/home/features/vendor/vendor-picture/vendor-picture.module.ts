
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { VendorPictureRoutingModule } from './vendor-picture-routing.module';
import { VendorPicturesComponent, VendorPictureAddComponent } from '.';

@NgModule({
  declarations: [
    VendorPicturesComponent,
    VendorPictureAddComponent
  ],
  entryComponents: [
  ],
  imports: [
    CommonModule,
    VendorPictureRoutingModule,
    SharedModule,
    TranslateModule
  ],
  providers: [
  ]
})

export class VendorPictureModule { }

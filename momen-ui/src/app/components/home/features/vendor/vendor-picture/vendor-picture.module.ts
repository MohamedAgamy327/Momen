
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { VendorPictureRoutingModule } from './vendor-picture-routing.module';
import { VendorPicturesComponent, VendorPictureAddComponent, VendorPictureShowDialogComponent } from '.';

@NgModule({
  declarations: [
    VendorPicturesComponent,
    VendorPictureAddComponent,
    VendorPictureShowDialogComponent
  ],
  entryComponents: [
    VendorPictureShowDialogComponent
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

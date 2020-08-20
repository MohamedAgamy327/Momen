import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VendorPicturesComponent, VendorPictureAddComponent } from '.';

const routes: Routes = [
  {
    path: '', children: [
      { path: '', component: VendorPicturesComponent },
      { path: 'add', component: VendorPictureAddComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorPictureRoutingModule { }

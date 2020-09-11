import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VendorsComponent, VendorAddComponent, StatusComponent } from '.';

const routes: Routes = [
  {
    path: '', children: [
      { path: '', component: VendorsComponent },
      { path: 'add', component: VendorAddComponent },
      { path: ':vendorId/status', component: StatusComponent },
      { path: ':vendorId/users', loadChildren: () => import('./vendor-user/vendor-user.module').then(m => m.VendorUserModule) },
      { path: ':vendorId/pictures', loadChildren: () => import('./vendor-picture/vendor-picture.module').then(m => m.VendorPictureModule) }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorRoutingModule { }

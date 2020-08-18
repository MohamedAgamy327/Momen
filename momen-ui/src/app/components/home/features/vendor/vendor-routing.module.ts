import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VendorsComponent } from '.';

const routes: Routes = [
  {
    path: '', children: [
      { path: '', component: VendorsComponent },
      { path: ':vendorId/users', loadChildren: () => import('./vendor-user/vendor-user.module').then(m => m.VendorUserModule) }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorRoutingModule { }

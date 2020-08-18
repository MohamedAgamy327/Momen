import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VendorUsersComponent } from '.';

const routes: Routes = [
  {
    path: '', component: VendorUsersComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VendorUserRoutingModule { }

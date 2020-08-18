import { AuthGuard } from '../../core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '.';

const routes: Routes = [
  {
    path: '', component: HomeComponent, children: [
      { path: 'categories', loadChildren: () => import('./features/category/category.module').then(m => m.CategoryModule) },
      { path: 'contracts', loadChildren: () => import('./features/contract/contract.module').then(m => m.ContractModule) },
      { path: 'vendors', loadChildren: () => import('./features/vendor/vendor.module').then(m => m.VendorModule) }
    ]
  },
  {
    path: '', redirectTo: '', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }

import { AuthGuard } from '../../core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
  HomeComponent, CategoriesComponent
} from '.';

const routes: Routes = [
  {
    path: '', component: HomeComponent, children: [
      { path: 'categories', component: CategoriesComponent }
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

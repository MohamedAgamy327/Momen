
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { TranslateModule } from '@ngx-translate/core';
import { CategoryRoutingModule } from './category-routing.module';
import { CategoryEditDialogComponent, CategoryAddDialogComponent, CategoriesComponent } from '.';


@NgModule({
  declarations: [
    CategoryAddDialogComponent,
    CategoryEditDialogComponent,
    CategoriesComponent
  ],
  entryComponents: [
    CategoryAddDialogComponent,
    CategoryEditDialogComponent
  ],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    SharedModule,
    TranslateModule
  ],
  providers: [
  ]
})

export class CategoryModule { }

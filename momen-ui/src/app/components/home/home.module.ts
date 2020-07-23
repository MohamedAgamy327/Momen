import {
  HomeComponent,
  DeleteDialogComponent,
  FooterComponent,
  HeaderComponent,
  LanguageDropDownComponent,
  SideBarComponent,
  CategoriesComponent,
  CategoryAddDialogComponent,
  CategoryEditDialogComponent
} from '.';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { TranslateModule } from '@ngx-translate/core';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [
    HomeComponent,
    DeleteDialogComponent,
    FooterComponent,
    HeaderComponent,
    LanguageDropDownComponent,
    SideBarComponent,
    CategoriesComponent,
    CategoryAddDialogComponent,
    CategoryEditDialogComponent
  ],
  entryComponents: [
    DeleteDialogComponent,
    CategoryAddDialogComponent,
    CategoryEditDialogComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModule,
    PerfectScrollbarModule,
    TranslateModule
  ],
  providers: [
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ]
})

export class HomeModule { }

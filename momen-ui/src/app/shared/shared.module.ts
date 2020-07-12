import { FlexLayoutModule } from '@angular/flex-layout';
import { AngularMaterialModule } from 'src/app/shared/material.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgxPrintModule } from 'ngx-print';
import { MenuToggleAnchorDirective, MenuToggleLinkDirective, MenuToggleDirective } from '../core/directives';
import { AddSpacePipe } from '../core/custom-pipes';
import { Ng5BreadcrumbModule } from 'ng5-breadcrumb';
import { ToastrModule } from 'ngx-toastr';
import { SlickCarouselModule } from 'ngx-slick-carousel';

@NgModule({
  declarations: [
    AddSpacePipe,
    MenuToggleAnchorDirective,
    MenuToggleLinkDirective,
    MenuToggleDirective
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    AngularMaterialModule,
    NgxPrintModule,
    FlexLayoutModule,
    Ng5BreadcrumbModule.forRoot(),
    ToastrModule.forRoot(),
    SlickCarouselModule
  ],
  exports: [
    AddSpacePipe,
    MenuToggleAnchorDirective,
    MenuToggleLinkDirective,
    MenuToggleDirective,
    ReactiveFormsModule,
    FormsModule,
    AngularMaterialModule,
    NgxPrintModule,
    FlexLayoutModule,
    Ng5BreadcrumbModule,
    ToastrModule,
    SlickCarouselModule
  ]
})
export class SharedModule { }

import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { MatTreeModule } from '@angular/material/tree';
import { CdkTreeModule } from '@angular/cdk/tree';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {
  MatTableModule, MatBottomSheetModule, MatSnackBarModule,
  MatNativeDateModule, MatCheckboxModule, MatMenuModule,
  MatButtonToggleModule, MatCardModule, MatDatepickerModule,
  MatDividerModule, MatExpansionModule, MatGridListModule,
  MatListModule, MatPaginatorModule, MatProgressBarModule,
  MatRadioModule, MatRippleModule, MatSelectModule, MatSliderModule,
  MatSlideToggleModule, MatSortModule, MatStepperModule,
  MatTabsModule, MatTooltipModule, MatFormFieldModule,
  MatAutocompleteModule, MatBadgeModule, MAT_DATE_LOCALE, MAT_DATE_FORMATS, DateAdapter, MAT_FORM_FIELD_DEFAULT_OPTIONS
} from '@angular/material';

import { MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { MomentUtcDateAdapter } from 'src/app/core/helpers';
import { TourMatMenuModule } from 'ngx-tour-md-menu';
import { MatGridListResponsive } from '../core/directives';
import { MaterialFileInputModule } from 'ngx-material-file-input';

@NgModule({
  declarations: [MatGridListResponsive],
  imports: [
    CdkTreeModule,
    MatInputModule,
    MatButtonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatDialogModule,
    MatTableModule,
    MatBottomSheetModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatMenuModule,
    MatNativeDateModule,
    MatChipsModule,
    MatButtonToggleModule,
    MatCardModule,
    MatDatepickerModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatListModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSortModule,
    MatStepperModule,
    MatTabsModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatTreeModule,
    MatBadgeModule,
    NgxMaterialTimepickerModule,
    NgxMatSelectSearchModule,
    TourMatMenuModule.forRoot(),
    MaterialFileInputModule
  ],
  exports: [
    MatInputModule, MatButtonModule, MatSidenavModule,
    MatToolbarModule, MatIconModule, MatDialogModule,
    MatTableModule, MatBottomSheetModule, MatSnackBarModule,
    MatProgressSpinnerModule, MatCheckboxModule, MatMenuModule,
    MatNativeDateModule, MatChipsModule, MatButtonToggleModule,
    MatCardModule, MatDatepickerModule, MatDividerModule,
    MatExpansionModule, MatGridListModule, MatListModule,
    MatPaginatorModule, MatProgressBarModule, MatRadioModule,
    MatRippleModule, MatSelectModule, MatSliderModule,
    MatSlideToggleModule, MatSortModule, MatStepperModule,
    MatTabsModule, MatTooltipModule, MatFormFieldModule,
    MatAutocompleteModule, MatTreeModule,
    MatBadgeModule, CdkTreeModule, NgxMaterialTimepickerModule,
    NgxMatSelectSearchModule, MatGridListResponsive, TourMatMenuModule,
    MaterialFileInputModule
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { floatLabel: 'always' } },
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
    { provide: DateAdapter, useClass: MomentUtcDateAdapter },
  ]
})

export class AngularMaterialModule { }

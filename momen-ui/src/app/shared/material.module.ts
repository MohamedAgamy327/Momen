import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { MatTreeModule } from '@angular/material/tree';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule, MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { MatBadgeModule } from '@angular/material/badge';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSliderModule } from '@angular/material/slider';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSortModule } from '@angular/material/sort';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatRadioModule } from '@angular/material/radio';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
import {
  MatRippleModule, MAT_DATE_LOCALE, MAT_DATE_FORMATS,
  DateAdapter, MatNativeDateModule
} from '@angular/material/core';
import { NgModule } from '@angular/core';
import { CdkTreeModule } from '@angular/cdk/tree';
import { MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { TourMatMenuModule } from 'ngx-tour-md-menu';
import { MomentUtcDateAdapter } from 'src/app/core/helpers';
import { MatGridListResponsive } from '../core/directives';

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

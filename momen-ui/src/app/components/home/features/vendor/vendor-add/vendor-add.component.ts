import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { VendorService, VendorPictureService, CategoryService, FileValidationService, PageTitleService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/core/models';
import { ActivatedRoute } from '@angular/router';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MatSelect } from '@angular/material/select';

@Component({
  selector: 'app-vendor-add',
  templateUrl: './vendor-add.component.html',
  styleUrls: ['./vendor-add.component.scss'],
  providers: [{ provide: STEPPER_GLOBAL_OPTIONS, useValue: { displayDefaultIndicatorType: false } }]
})

export class VendorAddComponent implements OnInit, OnDestroy {

  vendorForm: FormGroup;
  userForm: FormGroup;
  // attachedForm: FormGroup;

  @ViewChild('stepper') stepper: any;
  @ViewChild('logoInput') logoInput: any;
  @ViewChild('picturesInput') picturesInput: any;
  // @ViewChild('licenseInput') licenseInput: any;
  // @ViewChild('personalIdInput') personalIdInput: any;

  categories: Category[];

  public categoryMultiFilterCtrl: FormControl = new FormControl();
  public filteredCategoriesMulti: ReplaySubject<Category[]> = new ReplaySubject<Category[]>();
  @ViewChild('singleCategorySelect', { static: true }) singleCategorySelect: MatSelect;

  protected onDestroy = new Subject<void>();

  constructor(
    private pageTitleService: PageTitleService,
    public route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private vendorService: VendorService,
    private categoryService: CategoryService,
    private vendorPictureService: VendorPictureService,
    private fileValidationService: FileValidationService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.vendorForm = this.formBuilder.group({
      name: ['', Validators.required],
      categoryId: [null, Validators.required],
      branchesCount: [1, [Validators.required, Validators.min(1)]],
      description: ['', Validators.required],
      facebook: [''],
      twitter: [''],
      instgram: [''],
      logo: ['', Validators.required],
      logoSource: [''],
      pictures: ['', Validators.required],
      picturesSource: [''],
      vendorUser: []
    });

    this.userForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]*$')]]
    });

    // this.attachedForm = this.formBuilder.group({
    //   license: ['', Validators.required],
    //   licenseSource: [''],
    //   personalId: ['', Validators.required],
    //   personalIdSource: [''],
    // });
  }

  ngOnInit(): void {
    this.pageTitleService.setTitle('Vendor Add');
    this.getCategories();
  }

  getCategories() {
    this.categoryService.getAll().subscribe(
      (res: any) => {
        this.categories = res;
        this.getInitCategories();
      });
  }

  getInitCategories() {
    this.filteredCategoriesMulti.next(this.categories.slice());
    this.categoryMultiFilterCtrl.valueChanges
      .pipe(takeUntil(this.onDestroy))
      .subscribe(() => {
        this.filterCategoriesMulti();
      });
  }

  protected filterCategoriesMulti() {
    if (!this.categories) {
      return;
    }
    let search = this.categoryMultiFilterCtrl.value;
    if (!search) {
      this.filteredCategoriesMulti.next(this.categories.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    this.filteredCategoriesMulti.next(
      this.categories.filter(category => category.name.toLowerCase().indexOf(search) > -1)
    );
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  public vendorErrorHandling = (control: string, error: string) => {
    return this.vendorForm.controls[control].hasError(error);
  }

  public userErrorHandling = (control: string, error: string) => {
    return this.userForm.controls[control].hasError(error);
  }

  // public attachedErrorHandling = (control: string, error: string) => {
  //   return this.attachedForm.controls[control].hasError(error);
  // }

  logoInputChange(event: any) {
    if (this.fileValidationService.checkInvalidImage(event.target.files[0])) {
      this.vendorForm.patchValue({ logo: '' });
      this.toastrService.error('Invalid Image', 'Error');
    } else {
      this.vendorForm.patchValue({ logoSource: event.target.files[0] });
    }
  }

  clearLogoInput($event: any) {
    this.logoInput.clear($event);
    this.vendorForm.patchValue({ logo: '' });
  }

  picturesInputChange(event: any) {
    if (this.fileValidationService.checkInvalidImages(event.target.files)) {
      this.vendorForm.patchValue({ pictures: '' });
      this.toastrService.error('Invalid Images', 'Error');
    } else {
      this.vendorForm.patchValue({ picturesSource: event.target.files });
    }
  }

  clearPicturesInput($event: any) {
    this.picturesInput.clear($event);
    this.vendorForm.patchValue({ pictures: '' });
  }

  // licenseInputChange(event: any) {
  //   if (this.fileValidationService.checkInvalidImage(event.target.files[0])) {
  //     this.attachedForm.patchValue({ license: '' });
  //     this.toastrService.error('Invalid Image', 'Error');
  //   } else {
  //     this.attachedForm.patchValue({ licenseSource: event.target.files[0] });
  //   }
  // }

  // clearLicenseInput($event: any) {
  //   this.licenseInput.clear($event);
  //   this.attachedForm.patchValue({ license: '' });
  // }

  // personalIdInputChange(event: any) {
  //   if (this.fileValidationService.checkInvalidImage(event.target.files[0])) {
  //     this.attachedForm.patchValue({ personalId: '' });
  //     this.toastrService.error('Invalid Image', 'Error');
  //   } else {
  //     this.attachedForm.patchValue({ personalIdSource: event.target.files[0] });
  //   }
  // }

  // clearPersonalIdInput($event: any) {
  //   this.personalIdInput.clear($event);
  //   this.attachedForm.patchValue({ personalId: '' });
  // }

  save() {
    this.vendorForm.patchValue({ vendorUser: this.userForm.value });
    this.vendorService.create(this.vendorForm.value).subscribe(
      (res: any) => {
        this.uploadLogo(res.id);
        // this.uploadPersonalId(res.id);
        // this.uploadLicense(res.id);
        this.uploadPictures(res.id);
        this.stepper.reset();
        this.toastrService.success('Vendor Added Successfully', 'Add');
      });
  }

  uploadLogo(id: any) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('logo', this.vendorForm.value.logoSource, this.vendorForm.value.logoSource.name);
    this.vendorService.uploadLogo(Number(id), formData).subscribe(
      (res: any) => {
      });
  }

  uploadPictures(id: any) {
    const formData = new FormData();
    formData.append('vendorId', id);
    for (const picture of this.vendorForm.value.picturesSource) {
      formData.append('pictures', picture, picture.name);
    }
    this.vendorPictureService.create(formData).subscribe(
      (res: any) => {
      });
  }

  // uploadLicense(id: any) {
  //   const formData = new FormData();
  //   formData.append('id', id);
  //   formData.append('license', this.attachedForm.value.licenseSource, this.attachedForm.value.licenseSource.name);
  //   this.vendorService.uploadLicense(Number(id), formData).subscribe(
  //     (res: any) => {
  //     });
  // }

  // uploadPersonalId(id: any) {
  //   const formData = new FormData();
  //   formData.append('id', id);
  //   formData.append('personalId', this.attachedForm.value.personalIdSource, this.attachedForm.value.personalIdSource.name);
  //   this.vendorService.uploadPersonalId(Number(id), formData).subscribe(
  //     (res: any) => {
  //     });
  // }

}

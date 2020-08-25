import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { VendorUserService } from 'src/app/core/services';

@Component({
  selector: 'app-vendor-user-add-dialog',
  templateUrl: './vendor-user-add-dialog.component.html',
  styleUrls: ['./vendor-user-add-dialog.component.css']
})

export class VendorUserAddDialogComponent {

  addForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<VendorUserAddDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private vendorUserService: VendorUserService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.addForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      role: ['Admin', Validators.required],
      vendorId: [this.data]
    });
  }

  public errorHandling = (control: string, error: string) => {
    return this.addForm.controls[control].hasError(error);
  }

  save() {
    this.vendorUserService.create(this.addForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Added Successfully', 'Add');
        this.dialogRef.close(res);
      });
  }

}
